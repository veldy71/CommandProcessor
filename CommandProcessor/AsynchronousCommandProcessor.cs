using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class AsynchronousCommandProcessor.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	public abstract class AsynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse, TEvent>
		: CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>, 
		IAsynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse, TEvent>
		where TIdentifier : struct, IConvertible
		where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier,TStore> 
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore> 
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		where TEvent : class, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		/// <summary>
		/// The _message processing
		/// </summary>
		private bool _messageProcessing = false;

		/// <summary>
		/// The _commands awaiting response
		/// </summary>
		private readonly
			List<ICommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>> _commandsAwaitingResponse 
								= new List<ICommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>>();

		/// <summary>
		/// The _message processing thread
		/// </summary>
		private Thread _messageProcessingThread = null;
		/// <summary>
		/// The _message lock
		/// </summary>
		private readonly object _messageLock = new object();
		/// <summary>
		/// The _message reset event
		/// </summary>
		private readonly AutoResetEvent _messageResetEvent = new AutoResetEvent(false);
		/// <summary>
		/// The _message queue
		/// </summary>
		private readonly Queue<TStore> _messageQueue = new Queue<TStore> ();

		/// <summary>
		/// The _event processing thread
		/// </summary>
		private Thread _eventProcessingThread = null;
		/// <summary>
		/// The _event lock
		/// </summary>
		private readonly object _eventLock = new object();
		/// <summary>
		/// The _event reset event
		/// </summary>
		private readonly AutoResetEvent _eventResetEvent = new AutoResetEvent(false);

		/// <summary>
		/// Starts the processing.
		/// </summary>
		public override void StartProcessing()
		{
			base.StartProcessing();

			if (!_messageProcessing)
			{
				_messageProcessing = true;

				lock (_messageLock)
				{
					_messageProcessingThread = new Thread(ProcessMessages) { Priority = this.MessageThreadPriority, IsBackground = true };
					_messageProcessingThread.Start();
				}

				lock (_eventLock)
				{
					_eventProcessingThread = new Thread(ProcessEvents) { Priority = this.MessageThreadPriority, IsBackground = true };
					_eventProcessingThread.Start();
				}
			}
		}

		/// <summary>
		/// Stops the processing.
		/// </summary>
		public override void StopProcessing()
		{
			base.StopProcessing();

			if (_messageProcessing)
			{
				_messageProcessing = false;

				lock (_messageLock)
				{
					if (!_messageProcessingThread.Join((int)(this.MessageLatency * 1.1)))
						_messageProcessingThread.Abort();

					_messageProcessingThread = null;
				}

				lock (_eventLock)
				{
					if (!_eventProcessingThread.Join((int)(this.MessageLatency * 1.1)))
						_eventProcessingThread.Abort();

					_eventProcessingThread = null;
				}
			}
		}

		/// <summary>
		/// Gets the message latency.
		/// </summary>
		/// <value>The message latency.</value>
		protected virtual int MessageLatency { get { return 1500; } }

		/// <summary>
		/// Gets the message thread priority.
		/// </summary>
		/// <value>The message thread priority.</value>
		protected virtual ThreadPriority MessageThreadPriority { get { return ThreadPriority.Normal; } }

		/// <summary>
		/// Pushes the command without response asynchronus.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>System.Boolean.</returns>
		protected abstract bool PushCommandWithoutResponseAsynchronus(ICommand<TIdentifier, TStore> command);

		/// <summary>
		/// Pushes the command with response asynchronous.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns>System.Boolean.</returns>
		protected abstract bool PushCommandWithResponseAsync(ICommandWithResponse<TIdentifier, TStore, TResponse> commandWithResponse);

		/// <summary>
		/// Processes the commands.
		/// </summary>
		protected override void ProcessCommands()
		{
			while (_messageProcessing)
			{
				_ProcessCommandsResetEvent.WaitOne(this.CommandWait);

				ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>> transaction;

				do
				{
					transaction = null;

					lock (_CommandLock)
					{
						if (_CommandQueue.Any())
							transaction = _CommandQueue.Dequeue();

						// clear commands which found a response
						_commandsAwaitingResponse.RemoveAll(t => !t.WaitingForResponse);
					}

					// process the transaction
					if (transaction != null)
					{
						try
						{
							if (transaction.HasResponse)
							{
								var commandWithResponseTransaction = ((ICommandWithResponseTransaction
									<TIdentifier, TStore, TCommandWithResponse,
										TResponse>) transaction);

								var success = PushCommandWithResponseAsync(commandWithResponseTransaction.CommandWithResponse);
								if (success)
								{
									commandWithResponseTransaction.SetWaitingForResponse();

									lock (_CommandLock)
										_commandsAwaitingResponse.Add(commandWithResponseTransaction);
								}
							}
							else
							{
								if (PushCommandWithoutResponseAsynchronus(transaction.Command))
									transaction.SetInvactive();
							}
						}
						catch (Exception e)
						{
							transaction.SetException(e);
						}
					}
				} while (transaction != null);
			}
		}

		/// <summary>
		/// Processes the messages.
		/// </summary>
		private void ProcessMessages()
		{
			while (_messageProcessing)
			{
				_messageResetEvent.WaitOne(this.CommandWait);

				TStore message;

				do
				{
					message = null;
					lock (_messageLock)
					{
						if (_messageQueue.Any())
							message = _messageQueue.Dequeue();
					}

					if (message == null)
						continue;

					var handled = false;
					var transactionEnumerator = _commandsAwaitingResponse.Where(t => t.WaitingForResponse).GetEnumerator();
					while (!handled && transactionEnumerator.MoveNext())
					{
						var transaction = transactionEnumerator.Current;

						lock (_CommandLock)
						{
							if (transaction.SetResponseStore(message))
								handled = true;
						}
					}

					if (handled)
						continue;

					lock (_eventLock)
					{
						// TODO -- handle events
					}
				} 
				while (message != null);
			}
		}

		/// <summary>
		/// Processes the events.
		/// </summary>
		private void ProcessEvents()
		{
			while (_messageProcessing)
			{
				_eventResetEvent.WaitOne(this.CommandWait);

				// TODO
			}
		}

		/// <summary>
		/// Enqueues the message.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <exception cref="System.ArgumentNullException">store</exception>
		public void EnqueueMessage(TStore store)
		{
			if (store == null)
				throw new ArgumentNullException("store");

			lock (_messageLock)
			{
				_messageQueue.Enqueue(store);
				_messageResetEvent.Set();
			}
		}
	}
}
