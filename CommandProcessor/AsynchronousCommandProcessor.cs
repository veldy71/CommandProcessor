using System;
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
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		private bool _messageProcessing = false;

		private Thread _messageProcessingThread = null;
		private readonly object _messageLock = new object();

		private Thread _responseProcessingThread = null;
		private readonly object _responseLock = new object();

		private Thread _eventProcessingThread = null;
		private readonly object _eventLock = new object();

		/// <summary>
		/// Starts the incoming message processing.
		/// </summary>
		public void StartIncomingMessageProcessing()
		{
			if (_messageProcessing)
			{
				_messageProcessing = true;

				lock (_messageLock)
				{
					_messageProcessingThread = new Thread(ProcessMessages) { Priority = this.MessageThreadPriority, IsBackground = true};
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
		/// Stops the incoming message processing.
		/// </summary>
		public void StopIncomingMessageProcessing()
		{
			if (_messageProcessing)
			{
				_messageProcessing = false;

				lock (_messageLock)
				{
					if (!_messageProcessingThread.Join((int) (this.MessageLatency*1.1)))
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
			// TODO
		}

		/// <summary>
		/// Processes the messages.
		/// </summary>
		private void ProcessMessages()
		{
			// TODO
		}

		/// <summary>
		/// Processes the events.
		/// </summary>
		private void ProcessEvents()
		{
			// TODO
		}

		/// <summary>
		/// Processes the responses.
		/// </summary>
		private void ProcessResponses()
		{
			
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
