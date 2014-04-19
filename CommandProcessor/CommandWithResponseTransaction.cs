using System;
using System.Diagnostics;

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
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommandWithResponse<TIdentifier, TStore>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		where TStore : class
	{
		private readonly long _timeout;
		private bool _waitingForResponse = false;
		private static Stopwatch _stopwatch = null;

		private readonly ICommandWithResponse<TIdentifier, TStore> _commandWithResponse;
		private TResponse _response = null;
		private long _awaitingResponseSinceTimestamp = long.MinValue; // defaults to not waiting

		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="CommandWithResponseTransaction{TIdentifier, TStore, TCommand, TResponse}" /> class.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="timeout">The timeout.</param>
		public CommandWithResponseTransaction(TCommandWithResponse command, long timeout)
			: base(command)
		{
			_commandWithResponse = command;

			if (timeout < 1)
				throw new ArgumentOutOfRangeException("timeout");

			_timeout = timeout;
		}

		/// <summary>
		/// Initializes static members of the <see cref="CommandWithResponseTransaction{TIdentifier, TStore, TCommandWithResponse, TResponse}"/> class.
		/// </summary>
		static CommandWithResponseTransaction()
		{
			if (_stopwatch == null)
			{
				_stopwatch = new Stopwatch();
				_stopwatch.Start();
			}
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
		/// Sets the inactive.
		/// </summary>
		public override void SetInactive()
		{
			lock (_TransactionLock)
			{
				_awaitingResponseSinceTimestamp = long.MinValue;
				base.SetInactive();
			}
		}

		/// <summary>
		/// Gets a value indicating whether [response expired].
		/// </summary>
		/// <value><c>true</c> if [response expired]; otherwise, <c>false</c>.</value>
		public bool ResponseExpired
		{
			get
			{
				lock(_TransactionLock)
					return WaitingForResponse && (_stopwatch.ElapsedMilliseconds - _awaitingResponseSinceTimestamp) > _timeout;
			}
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

		/// <summary>
		///     Gets a value indicating whether [waiting for response].
		/// </summary>
		/// <value><c>true</c> if [waiting for response]; otherwise, <c>false</c>.</value>
		public bool WaitingForResponse
		{
			get
			{
				lock (_TransactionLock)
					return _waitingForResponse && IsActive && Response == null;
			}
		}

		/// <summary>
		///     Sets the waiting for response.
		/// </summary>
		public void SetWaitingForResponse()
		{
			if (Response == null)
			{
				lock (_TransactionLock)
				{
					_awaitingResponseSinceTimestamp = _stopwatch.ElapsedMilliseconds;
					_waitingForResponse = true;
				}
			}
		}

		/// <summary>
		///     Gets the awaiting response since timestamp.
		/// </summary>
		/// <value>The awaiting response since timestamp.</value>
		private long AwaitingResponseSinceTimestamp
		{
			get
			{
				return _awaitingResponseSinceTimestamp;
			}
		}
	}
}