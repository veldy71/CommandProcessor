using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
	public abstract class AsynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		: CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>, 
		IAsynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible
		where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier,TStore> 
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore> 
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
	{
		private bool _messageProcessing = false;

		private readonly
			List<ICommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>> _commandsAwaitingResponse 
								= new List<ICommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>>();

		private Thread _messageProcessingThread = null;
		private readonly object _messageLock = new object();
		private readonly AutoResetEvent _messageResetEvent = new AutoResetEvent(false);
		private readonly Queue<TStore> _messageQueue = new Queue<TStore> ();

		private Thread _responseProcessingThread = null;
		private readonly object _responseLock = new object();
		private readonly AutoResetEvent _responseResetEvent = new AutoResetEvent(false);

		private Thread _eventProcessingThread = null;
		private readonly object _eventLock = new object();
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

				lock (_responseLock)
				{
					_responseProcessingThread = new Thread(ProcessResponses) { Priority = this.MessageThreadPriority, IsBackground = true };
					_responseProcessingThread.Start();
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

				lock (_responseLock)
				{
					if (!_responseProcessingThread.Join((int)(this.MessageLatency * 1.1)))
						_responseProcessingThread.Abort();

					_responseProcessingThread = null;
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
		/// Processes the commands.
		/// </summary>
		protected override void ProcessCommands()
		{
			while (_messageProcessing)
			{
				_ProcessCommandsResetEvent.WaitOne(this.CommandWait);

				ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>> transaction = null;

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
			}
		}

		/// <summary>
		/// Pushes the command without response asynchronus.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>System.Boolean.</returns>
		protected abstract bool PushCommandWithoutResponseAsynchronus(ICommand<TIdentifier, TStore> command);

		/// <summary>
		/// Pushes the command with response asynchronous.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <returns>System.Boolean.</returns>
		protected abstract bool PushCommandWithResponseAsync(ICommandWithResponse<TIdentifier, TStore, TResponse> transaction);

		/// <summary>
		/// Processes the messages.
		/// </summary>
		private void ProcessMessages()
		{
			while (_messageProcessing)
			{
				_messageResetEvent.WaitOne(this.CommandWait);

				TStore message = null;
				lock (_messageLock)
				{
					if (_messageQueue.Any())
						message = _messageQueue.Dequeue();
				}

				if (message == null)
					continue;

				var handled = false;
				var transactionEnumerator = _commandsAwaitingResponse.Where(t => t.WaitingForResponse).GetEnumerator();
				while(!handled && transactionEnumerator.MoveNext())
				{
					var transaction = transactionEnumerator.Current;

					lock (_CommandLock)
					{
						if (transaction.SetResponseStore(message))
							handled = true;
					}
				}

				lock (_eventLock)
				{
					// TODO -- process unhandled message for event matches
				}
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
		/// Processes the responses.
		/// </summary>
		private void ProcessResponses()
		{
			while (_messageProcessing)
			{
				_responseResetEvent.WaitOne(this.CommandWait);

				// TODO
			}
		}

		/// <summary>
		/// Pushes the command with response.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns></returns>
		protected abstract void PushCommandWithResponse(ICommandWithResponse<TIdentifier, TStore, TResponse> commandWithResponse);

		/// <summary>
		/// Pushes the command without response.
		/// </summary>
		/// <param name="command">The command.</param>
		protected abstract void PushCommandWithoutResponse(ICommand<TIdentifier, TStore> command);

		public void EnqueueMessage(TStore store)
		{
			
		}
	}
}
