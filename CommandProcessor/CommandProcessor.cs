﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Class CommandProcessor.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public abstract class CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		: ICommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
	{
		protected readonly object _CommandLock = new object();
		protected readonly Queue<ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>>> _CommandQueue
			= new Queue<ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>>>();
		protected readonly AutoResetEvent _ProcessCommandsResetEvent = new AutoResetEvent(false);
		private readonly Stopwatch _timeoutStopwatch = new Stopwatch();
		private Thread _commandProcessingThread;
		private bool _disposed;
		private bool _isProcessingCommands;
		public const ThreadPriority DefaultCommunicationThreadPriority = ThreadPriority.AboveNormal;
		public const int DefaultCommandTimeout = 10000;
		public const int DefaultCommandWait = 100;

		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="CommandProcessor{TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse}" /> class.
		/// </summary>
		protected CommandProcessor()
		{
			_timeoutStopwatch.Start();
			CommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>.SetTimeoutStopwatch(
				_timeoutStopwatch);
		}

		/// <summary>
		///     Gets the communication thread priority.
		/// </summary>
		/// <value>The communication thread priority.</value>
		protected virtual ThreadPriority CommunicationThreadPriority
		{
			get { return DefaultCommunicationThreadPriority; }
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
		///     Gets the command wait time in milliseconds.
		/// </summary>
		/// <value>The command wait.</value>
		public virtual int CommandWait
		{
			get { return DefaultCommandWait; }
		}

		/// <summary>
		///     Gets the command timeout.
		/// </summary>
		/// <value>The command timeout.</value>
		public virtual int CommandTimeout
		{
			get { return DefaultCommandTimeout; }
		}

		/// <summary>
		///     Gets a value indicating whether [is processing messages].
		/// </summary>
		/// <value><c>true</c> if [is processing messages]; otherwise, <c>false</c>.</value>
		public bool IsProcessingMessages
		{
			get { return _isProcessingCommands; }
		}

		/// <summary>
		///     Sends the command.
		/// </summary>
		/// <typeparam name="TRsp">The type of the t RSP.</typeparam>
		/// <param name="command">The command.</param>
		/// <returns>``0.</returns>
		/// <exception cref="System.TimeoutException"></exception>
		public TRsp SendCommandWithResponse<TRsp>(ICommandWithResponse<TIdentifier, TStore, TRsp> command)
			where TRsp : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		{
			// create the transaction
			var transaction =
				new CommandWithResponseTransaction<TIdentifier, TStore, ICommandWithResponse<TIdentifier, TStore, TRsp>, TRsp>(
					command, CommandTimeout);

			try
			{
				lock (_CommandLock)
				{
					// enqueue it for processing
					_CommandQueue.Enqueue(transaction);

					// let the processing thread know to do some work
					_ProcessCommandsResetEvent.Set();
				}

				// wait for the background worker to process the command
				bool signaled = transaction.ResetEvent.WaitOne(CommandTimeout);
				if (!signaled)
					throw new TimeoutException();

				// if there was an exception on the background thread, throw it here
				if (transaction.Exception != null)
					throw transaction.Exception;

				return transaction.Response;
			}
			finally
			{
				lock (_CommandLock)
				{
					transaction.Dispose();
				}
			}
		}

		/// <summary>
		///     Sends the command without a response.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <exception cref="System.TimeoutException"></exception>
		public void SendCommand(TCommand command)
		{
			// create the transaction
			var transaction = new CommandTransaction<TIdentifier, TStore, TCommand>(command);

			try
			{
				lock (_CommandLock)
				{
					// enqueue it for processing
					_CommandQueue.Enqueue(transaction);

					// let the processing thread know to do some work
					_ProcessCommandsResetEvent.Set();
				}

				bool signaled = transaction.ResetEvent.WaitOne(CommandTimeout);
				if (!signaled)
					throw new TimeoutException();

				// if there was an exception on the background thread, throw it here
				if (transaction.Exception != null)
					throw transaction.Exception;
			}
			finally
			{
				lock (_CommandLock)
				{
					transaction.Dispose();
				}
			}
		}

		/// <summary>
		///     Starts the command processing.
		/// </summary>
		public virtual void StartProcessing()
		{
			if (!IsProcessingMessages)
			{
				_isProcessingCommands = true;
				_commandProcessingThread = new Thread(ProcessCommands) {Priority = CommunicationThreadPriority, IsBackground = true};
				_commandProcessingThread.Start();
			}
		}

		/// <summary>
		///     Stops the command processing.
		/// </summary>
		public virtual void StopProcessing()
		{
			if (IsProcessingMessages)
			{
				_isProcessingCommands = false;
				bool signaled = _commandProcessingThread.Join(CommandWait);
				if (!signaled)
					_commandProcessingThread.Abort();

				_commandProcessingThread = null;
			}
		}

		/// <summary>
		///     Finalizes an instance of the
		///     <see cref="CommandProcessor{TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse}" /> class.
		/// </summary>
		~CommandProcessor()
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
				}

				_timeoutStopwatch.Stop();

				if (_isProcessingCommands)
					StopProcessing();

				_disposed = true;
			}
		}

		/// <summary>
		///     Processes the commands.
		/// </summary>
		protected abstract void ProcessCommands();
	}
}