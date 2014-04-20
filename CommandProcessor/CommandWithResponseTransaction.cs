using System;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Class CommandWithResponseTransaction.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	class CommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>
		: CommandTransaction<TIdentifier, TStore, TCommandWithResponse>,
			ICommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		where TStore : class
	{
		private readonly TCommandWithResponse _commandWithResponse;
		private TResponse _response = null;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="CommandWithResponseTransaction{TIdentifier, TStore, TCommand, TResponse}" /> class.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="resetEvent">The reset event.</param>
		private CommandWithResponseTransaction(TCommandWithResponse command, AutoResetEvent resetEvent)
			: base(command, resetEvent)
		{
			_commandWithResponse = command;

		}

		/// <summary>
		/// Creates the specified command with response.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns>ICommandWithResponseTransaction&lt;TIdentifier, TStore, TCommandWithResponse&gt;.</returns>
		public static new ICommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse> Create(
			TCommandWithResponse commandWithResponse)
		{
			return new CommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>(commandWithResponse, AcquireResetEvent());
		}

		/// <summary>
		///     Gets the command with response.
		/// </summary>
		/// <value>
		///     The command with response.
		/// </value>
		public ICommandWithResponse<TIdentifier, TStore> CommandWithResponse
		{
			get
			{
				lock (_TransactionLock)
					return _commandWithResponse;
			}
		}

		/// <summary>
		///     Gets the response.
		/// </summary>
		/// <value>
		///     The response.
		/// </value>
		public TResponse Response
		{
			get
			{
				lock (_TransactionLock)
					return _response;
			}
		}

		/// <summary>
		///     Gets a value indicating whether [has response].
		/// </summary>
		/// <value>
		///     <c>true</c> if [has response]; otherwise, <c>false</c>.
		/// </value>
		public override bool HasResponse
		{
			get { return true; }
		}

		/// <summary>
		///     Sets the response store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <returns></returns>
		public bool SetResponseStore(TStore store)
		{
			lock (_TransactionLock)
			{
				var response = ((ICommandWithResponse<TIdentifier, TStore, TResponse>) CommandWithResponse).CreateResponse();

				if (response.Key.CompareTo(store) == 0)
				{
					_response = response;
					_response.SetStore(store);
					SetInactive();

					return true;
				}

				return false;
			}
		}
	}
}