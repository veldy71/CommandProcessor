﻿using System;
using System.Linq;

namespace Veldy.Net.CommandProcessor
{
	public abstract class SynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		: CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible
		where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		/// <summary>
		/// Processes the commands.
		/// </summary>
		protected override void ProcessCommands()
		{
			while (this.IsProcessingMessages)
			{
				ProcessCommandsResetEvent.WaitOne(this.CommandWait);

				ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>> transaction = null;

				lock (CommandLock)
				{
					if (_CommandQueue.Any())
						transaction = _CommandQueue.Dequeue();
				}

				// process the transaction
				if (transaction != null)
				{
					try
					{
						if (transaction.HasResponse)
						{
							var commandWithResponseTransaction = ((ICommandWithResponseTransaction
								<TIdentifier, TStore, ICommandWithResponse<TIdentifier, TStore, TResponse>,
									TResponse>)transaction);

							var responseStore = PushCommandWithResponse(commandWithResponseTransaction.CommandWithResponse);
							commandWithResponseTransaction.SetResponseStore(responseStore);
						}
						else
						{
							PushCommandWithoutResponse(transaction.Command);
						}

						transaction.SetInvactive();
					}
					catch (Exception e)
					{
						transaction.SetException(e);
					}
				}
			}
		}
	}
}
