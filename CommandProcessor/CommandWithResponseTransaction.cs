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
	public class CommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>
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

		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="CommandWithResponseTransaction{TIdentifier, TStore, TCommand, TResponse}" /> class.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="timeout">The timeout.</param>
		public CommandWithResponseTransaction(TCommandWithResponse command, long timeout)
			: base(command)
		{
			CommandWithResponse = command;

			if (timeout < 1)
				throw new ArgumentOutOfRangeException("timeout");

			_timeout = timeout;

			AwaitingResponseSinceTimestamp = long.MinValue; // defaults to not waiting
		}

		/// <summary>
		/// Initializes static members of the <see cref="CommandWithResponseTransaction{TIdentifier, TStore, TCommandWithResponse, TResponse}"/> class.
		/// </summary>
		static CommandWithResponseTransaction()
		{
			if (TimeoutStopWatch == null)
			{
				TimeoutStopWatch = new Stopwatch();
				TimeoutStopWatch.Start();
			}
		}

		/// <summary>
		///     Gets the timeout stop watch.
		/// </summary>
		/// <value>The timeout stop watch.</value>
		private static Stopwatch TimeoutStopWatch { get; set; }

		/// <summary>
		///     Gets the command with response.
		/// </summary>
		/// <value>
		///     The command with response.
		/// </value>
		public ICommandWithResponse<TIdentifier, TStore> CommandWithResponse { get; private set; }

		/// <summary>
		///     Gets the response.
		/// </summary>
		/// <value>
		///     The response.
		/// </value>
		public TResponse Response { get; private set; }

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
			AwaitingResponseSinceTimestamp = long.MinValue;
			base.SetInactive();
		}

		/// <summary>
		/// Gets a value indicating whether [response expired].
		/// </summary>
		/// <value><c>true</c> if [response expired]; otherwise, <c>false</c>.</value>
		public bool ResponseExpired
		{
			get
			{
				return WaitingForResponse && (TimeoutStopWatch.ElapsedMilliseconds - AwaitingResponseSinceTimestamp) > _timeout;
			}
		}

		/// <summary>
		///     Sets the response store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <returns></returns>
		public bool SetResponseStore(TStore store)
		{
			var response = ((ICommandWithResponse<TIdentifier, TStore, TResponse>)CommandWithResponse).CreateResponse();

			if (response.Key.CompareTo(store) == 0)
			{
				Response = response;
				Response.SetStore(store);
				SetInactive();

				return true;
			}

			return false;
		}

		/// <summary>
		///     Gets a value indicating whether [waiting for response].
		/// </summary>
		/// <value><c>true</c> if [waiting for response]; otherwise, <c>false</c>.</value>
		public bool WaitingForResponse { get { return _waitingForResponse && IsActive && Response == null; } }

		/// <summary>
		///     Sets the waiting for response.
		/// </summary>
		public void SetWaitingForResponse()
		{
			if (Response == null)
			{
				var elapsedTime = TimeoutStopWatch.ElapsedMilliseconds;

				AwaitingResponseSinceTimestamp = TimeoutStopWatch.ElapsedMilliseconds;
				_waitingForResponse = true;
			}
		}

		/// <summary>
		///     Gets the awaiting response since timestamp.
		/// </summary>
		/// <value>The awaiting response since timestamp.</value>
		public long AwaitingResponseSinceTimestamp { get; private set; }
	}
}