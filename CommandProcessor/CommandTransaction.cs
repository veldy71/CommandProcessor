using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Class CommandTransaction.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	class CommandTransaction<TIdentifier, TStore, TCommand>
		: ICommandTransaction<TIdentifier, TStore, TCommand>
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
		private bool _disposed = false;
		protected readonly object _TransactionLock = new object();

		private readonly TCommand _command;
		private bool _isActive = true;
		private readonly AutoResetEvent _resetEvent;
		private Exception _exception = null;

		private static readonly ConcurrentStack<AutoResetEvent> _resetEvents = new ConcurrentStack<AutoResetEvent>();

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandTransaction{TIdentifier, TStore, TCommand}" /> class.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="resetEvent">The reset event.</param>
		/// <exception cref="System.ArgumentNullException">command</exception>
		protected CommandTransaction(TCommand command, AutoResetEvent resetEvent)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			_command = command;
			_resetEvent = resetEvent;
		}

		/// <summary>
		/// Creates the specified command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>ICommandTransaction&lt;TIdentifier, TStore, TCommand&gt;.</returns>
		public static ICommandTransaction<TIdentifier, TStore, TCommand> Create(TCommand command)
		{
			return new CommandTransaction<TIdentifier, TStore, TCommand>(command, AcquireResetEvent());
		}

		/// <summary>
		/// Acquires the reset event.
		/// </summary>
		/// <returns>AutoResetEvent.</returns>
		protected static AutoResetEvent AcquireResetEvent()
		{
			AutoResetEvent resetEvent;
			if (_resetEvents.Any() && _resetEvents.TryPop(out resetEvent))
				return resetEvent;
			else
				return new AutoResetEvent(false);
		}

		/// <summary>
		/// Releases the reset event.
		/// </summary>
		/// <param name="resetEvent">The reset event.</param>
		protected static void ReleaseResetEvent(AutoResetEvent resetEvent)
		{
			_resetEvents.Push(resetEvent);
		}

		/// <summary>
		///     Gets the command.
		/// </summary>
		/// <value>
		///     The command.
		/// </value>
		public TCommand Command
		{
			get
			{
				lock(_TransactionLock)
					return _command;	
			}
		}

		/// <summary>
		///     Gets a value indicating whether [is active].
		/// </summary>
		/// <value>
		///     <c>true</c> if [is active]; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive
		{
			get
			{
				lock (_TransactionLock)
					return _isActive;
			}
		}

		/// <summary>
		///     Gets the reset event.
		/// </summary>
		/// <value>
		///     The reset event.
		/// </value>
		public WaitHandle ResetEvent
		{
			get
			{
				return _resetEvent;
			}
		}

		/// <summary>
		///     Gets the exception.
		/// </summary>
		/// <value>
		///     The exception.
		/// </value>
		public Exception Exception
		{
			get
			{
				lock (_TransactionLock)
					return _exception;
			}
		}

		/// <summary>
		///     Gets a value indicating whether [has response].
		/// </summary>
		/// <value>
		///     <c>true</c> if [has response]; otherwise, <c>false</c>.
		/// </value>
		public virtual bool HasResponse
		{
			get { return false; }
		}

		/// <summary>
		///     Sets the inactive.
		/// </summary>
		public virtual void SetInactive()
		{
			lock (_TransactionLock)
			{
				_isActive = false;
				_resetEvent.Set();
			}
		}

		/// <summary>
		///     Sets the exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		public void SetException(Exception exception)
		{
			lock (_TransactionLock)
			{
				_exception = exception;
				SetInactive();
			}
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///     Finalizes an instance of the <see cref="CommandTransaction{TIdentifier, TStore, TCommand}" /> class.
		/// </summary>
		~CommandTransaction()
		{
			Dispose(false);
		}

		/// <summary>
		///     Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing">
		///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
		///     unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					lock (_TransactionLock)
						ReleaseResetEvent(_resetEvent);
				}

				_disposed = true;
			}
		}
	}
}