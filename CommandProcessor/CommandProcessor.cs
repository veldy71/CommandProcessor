using System;
using System.Collections.Generic;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class CommandProcessor.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public abstract class CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		: ICommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible
		where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		private bool _isProcessingCommands = false;
		private Thread _commandProcessingThread = null;
		protected readonly AutoResetEvent ProcessCommandsResetEvent = new AutoResetEvent(false);
		protected readonly object CommandLock = new object();

		protected readonly Queue<ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>>> _CommandQueue
			= new Queue<ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>>>();

		/// <summary>
		/// Gets the command wait time in milliseconds.
		/// </summary>
		/// <value>
		/// The command wait.
		/// </value>
		public virtual int CommandWait
		{
			get { return 100; }
		}

		/// <summary>
		/// Gets the command timeout.
		/// </summary>
		/// <value>
		/// The command timeout.
		/// </value>
		public virtual int CommandTimeout
		{
			get { return 10000; }
		}

		/// <summary>
		/// Gets a value indicating whether [is processing messages].
		/// </summary>
		/// <value>
		/// <c>true</c> if [is processing messages]; otherwise, <c>false</c>.
		/// </value>
		public bool IsProcessingMessages
		{
			get { return _isProcessingCommands; }
		}

		/// <summary>
		/// Gets the communication thread priority.
		/// </summary>
		/// <value>The communication thread priority.</value>
		protected virtual ThreadPriority CommunicationThreadPriority { get { return ThreadPriority.AboveNormal; } }

		/// <summary>
		/// Sends the command with a response.
		/// </summary>
		/// <typeparam name="TCmd">The type of the command.</typeparam>
		/// <typeparam name="TRsp">The type of the RSP.</typeparam>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		/// <exception cref="System.TimeoutException"></exception>
		public TRsp SendCommand<TCmd, TRsp>(TCmd command)
			where TCmd : class, TCommandWithResponse, ICommandWithResponse<TIdentifier, TStore, TRsp>,
				ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
			where TRsp : class, TResponse, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		{
			// create the transaction
			var transaction =
				new CommandWithResponseTransaction<TIdentifier, TStore, ICommandWithResponse<TIdentifier, TStore, TRsp>, TRsp>(
					command);

			try
			{
				lock (CommandLock)
				{
					// enqueue it for processing
					_CommandQueue.Enqueue(transaction);

					// let the processing thread know to do some work
					ProcessCommandsResetEvent.Set();
				}

				// wait for the background worker to process the command
				var signaled = transaction.ResetEvent.WaitOne(this.CommandTimeout);
				if (!signaled)
					throw new TimeoutException();

				// if there was an exception on the background thread, throw it here
				if (transaction.Exception != null)
					throw transaction.Exception;

				return transaction.Response;
			}
			finally
			{
				lock (CommandLock)
				{
					transaction.Dispose();
				}
			}
		}

		/// <summary>
		/// Sends the command without a response.
		/// </summary>
		/// <param name="command">The command.</param>
		public void SendCommand(TCommand command)
		{
			// create the transaction
			var transaction = new CommandTransaction<TIdentifier, TStore, TCommand>(command);

			try
			{
				lock (CommandLock)
				{
					// enqueue it for processing
					_CommandQueue.Enqueue(transaction);

					// let the processing thread know to do some work
					ProcessCommandsResetEvent.Set();
				}

				var signaled = transaction.ResetEvent.WaitOne(this.CommandTimeout);
				if (!signaled)
					throw new TimeoutException();

				// if there was an exception on the background thread, throw it here
				if (transaction.Exception != null)
					throw transaction.Exception;
			}
			finally
			{
				lock (CommandLock)
				{
					transaction.Dispose();
				}
			}
		}

		/// <summary>
		/// Starts the command processing.
		/// </summary>
		public virtual void StartProcessing()
		{
			if (!IsProcessingMessages)
			{
				_isProcessingCommands = true;
				_commandProcessingThread = new Thread(ProcessCommands) {Priority = this.CommunicationThreadPriority, IsBackground = true};
				_commandProcessingThread.Start();
			}
		}

		/// <summary>
		/// Stops the command processing.
		/// </summary>
		public virtual void StopProcessing()
		{
			if (IsProcessingMessages)
			{
				_isProcessingCommands = false;
				var signaled = _commandProcessingThread.Join(this.CommandWait);
				if (!signaled)
					_commandProcessingThread.Abort();

				_commandProcessingThread = null;
			}
		}

		/// <summary>
		/// Processes the commands.
		/// </summary>
		protected abstract void ProcessCommands();
	}
}
