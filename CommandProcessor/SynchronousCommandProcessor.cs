using System;
using System.Linq;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Class SynchronousCommandProcessor.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
	public abstract class SynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		: CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>, 
		ISynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
	{
		/// <summary>
		///     Processes the commands.
		/// </summary>
		protected override void ProcessCommands()
		{
			while (IsProcessingCommands)
			{
				_ProcessCommandsResetEvent.WaitOne(CommandWait);

				ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>> transaction = null;

				if (_CommandQueue.Any())
					if (!_CommandQueue.TryDequeue(out transaction))
						transaction = null;

				// process the transaction
				if (transaction != null)
				{
					try
					{
						if (transaction.HasResponse)
						{
							var commandWithResponseTransaction = ((ICommandWithResponseTransaction<TIdentifier, TStore, ICommandWithResponse<TIdentifier, TStore>>) transaction);

							var responseStore = PushCommandWithResponse(commandWithResponseTransaction.CommandWithResponse);
							commandWithResponseTransaction.SetResponseStore(responseStore);
						}
						else
						{
							PushCommandWithoutResponse(transaction.Command);
						}

						transaction.SetInactive();
					}
					catch (Exception e)
					{
						transaction.SetException(e);
					}
				}
			}
		}

		/// <summary>
		///     Pushes the command with response.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns></returns>
		protected abstract TStore PushCommandWithResponse(ICommandWithResponse<TIdentifier, TStore> commandWithResponse);

		/// <summary>
		///     Pushes the command without response.
		/// </summary>
		/// <param name="command">The command.</param>
		protected abstract void PushCommandWithoutResponse(ICommand<TIdentifier, TStore> command);
	}
}