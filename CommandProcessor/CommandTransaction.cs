using System;
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
		private ManualResetEvent _manualResetEvent = new ManualResetEvent(false);
		private Exception _exception = null;

		/// <summary>
		///     Initializes a new instance of the <see cref="CommandTransaction{TIdentifier, TStore, TCommand}" /> class.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <exception cref="System.ArgumentNullException">command</exception>
		public CommandTransaction(TCommand command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			_command = command;
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
		public ManualResetEvent ResetEvent
		{
			get
			{
				return _manualResetEvent;
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
				ResetEvent.Set();
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
					{
						if (_manualResetEvent != null)
						{
							_manualResetEvent.Dispose();
							_manualResetEvent = null;
						}
					}
				}

				_disposed = true;
			}
		}
	}
}