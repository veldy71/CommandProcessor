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
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		where TStore : class
	{
		private readonly int _timeout;
		private bool _waitingForResponse;

		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="CommandWithResponseTransaction{TIdentifier, TStore, TCommand, TResponse}" /> class.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="timeout">The timeout.</param>
		public CommandWithResponseTransaction(TCommandWithResponse command, int timeout)
			: base(command)
		{
			CommandWithResponse = command;
			Response = new TResponse();

			if (timeout < 1)
				throw new ArgumentOutOfRangeException("timeout");

			_timeout = timeout;
		}

		/// <summary>
		///     Gets the timeout stop watch.
		/// </summary>
		/// <value>The timeout stop watch.</value>
		protected static Stopwatch TimeoutStopWatch { get; private set; }

		/// <summary>
		///     Gets the command with response.
		/// </summary>
		/// <value>
		///     The command with response.
		/// </value>
		public TCommandWithResponse CommandWithResponse { get; private set; }

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
		///     Sets the response store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <returns></returns>
		public bool SetResponseStore(TStore store)
		{
			IsActive = false;

			if (Response.Key.CompareTo(store) == 0)
			{
				Response.SetStore(store);
				SetInvactive();
				return true;
			}

			return false;
		}

		/// <summary>
		///     Gets a value indicating whether [waiting for response].
		/// </summary>
		/// <value><c>true</c> if [waiting for response]; otherwise, <c>false</c>.</value>
		public bool WaitingForResponse
		{
			get
			{
				return _waitingForResponse
				       && (TimeoutStopWatch.ElapsedMilliseconds - AwaitingResponseSinceTimestamp) < _timeout;
			}
		}

		/// <summary>
		///     Sets the waiting for response.
		/// </summary>
		public void SetWaitingForResponse()
		{
			if (Response == null)
			{
				_waitingForResponse = true;
				AwaitingResponseSinceTimestamp = TimeoutStopWatch.ElapsedMilliseconds;
			}
		}

		/// <summary>
		///     Gets the awaiting response since timestamp.
		/// </summary>
		/// <value>The awaiting response since timestamp.</value>
		public long AwaitingResponseSinceTimestamp { get; private set; }

		/// <summary>
		///     Sets the timeout stopwatch.
		/// </summary>
		/// <param name="stopwatch">The stopwatch.</param>
		/// <exception cref="System.ArgumentNullException">stopwatch</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">stopwatch</exception>
		public static void SetTimeoutStopwatch(Stopwatch stopwatch)
		{
			if (stopwatch == null)
				throw new ArgumentNullException("stopwatch");

			if (stopwatch != null && TimeoutStopWatch != null)
				throw new ArgumentOutOfRangeException("stopwatch");

			TimeoutStopWatch = stopwatch;
		}
	}
}