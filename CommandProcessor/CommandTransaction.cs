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
	public class CommandTransaction<TIdentifier, TStore, TCommand>
		: ICommandTransaction<TIdentifier, TStore, TCommand>
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
		private bool _disposed = false;

		/// <summary>
		///     Initializes a new instance of the <see cref="CommandTransaction{TIdentifier, TStore, TCommand}" /> class.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <exception cref="System.ArgumentNullException">command</exception>
		public CommandTransaction(TCommand command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			Command = command;
			IsActive = true;
			Exception = null;
			ResetEvent = new ManualResetEvent(false);
		}

		/// <summary>
		///     Gets the command.
		/// </summary>
		/// <value>
		///     The command.
		/// </value>
		public TCommand Command { get; private set; }

		/// <summary>
		///     Gets a value indicating whether [is active].
		/// </summary>
		/// <value>
		///     <c>true</c> if [is active]; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive { get; protected set; }

		/// <summary>
		///     Gets the reset event.
		/// </summary>
		/// <value>
		///     The reset event.
		/// </value>
		public ManualResetEvent ResetEvent { get; private set; }

		/// <summary>
		///     Gets the exception.
		/// </summary>
		/// <value>
		///     The exception.
		/// </value>
		public Exception Exception { get; private set; }

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
			IsActive = false;
			ResetEvent.Set();
		}

		/// <summary>
		///     Sets the exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		public void SetException(Exception exception)
		{
			SetInactive();
			Exception = exception;
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Dispose(true);
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
		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					if (ResetEvent != null)
					{
						ResetEvent.Dispose();
						ResetEvent = null;
					}
				}

				_disposed = true;
			}
		}
	}
}