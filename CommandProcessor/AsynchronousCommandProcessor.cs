using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Class AsynchronousCommandProcessor.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	public abstract class AsynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse,
		TEvent>
		: CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>,
			IAsynchronousCommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse, TEvent>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommandWithResponse<TIdentifier, TStore>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		where TEvent : class, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		private readonly List<IEventAction<TIdentifier, TStore>> _eventActions = new List<IEventAction<TIdentifier, TStore>>();
		private readonly object _eventLock = new object();

		private readonly Queue<Tuple<IEventAction<TIdentifier, TStore>, TEvent>> _eventQueue =
			new Queue<Tuple<IEventAction<TIdentifier, TStore>, TEvent>>();

		private readonly AutoResetEvent _eventResetEvent = new AutoResetEvent(false);

		private readonly object _messageLock = new object();
		private readonly Queue<TStore> _messageQueue = new Queue<TStore>();
		private readonly AutoResetEvent _messageResetEvent = new AutoResetEvent(false);

		private Thread _eventProcessingThread;
		private Thread _messageProcessingThread;

		private const ThreadPriority DefautMessageThreadPriority = ThreadPriority.Normal;

		private const int DefaultMessageWait = 1000;
		private const int DefaultEventWait = 1000;

		private readonly ConcurrentQueue<ICommandTransaction<TIdentifier, TStore, TCommand>> _commandTransactions = new ConcurrentQueue<ICommandTransaction<TIdentifier, TStore, TCommand>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="AsynchronousCommandProcessor{TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse, TEvent}"/> class.
		/// </summary>
		protected AsynchronousCommandProcessor()
		{
			IsProcessingMessages = false;
			IsProcessingEvents = false;
		}

		/// <summary>
		/// Gets the event wait.
		/// </summary>
		/// <value>The event wait.</value>
		public virtual int EventWait
		{
			get { return DefaultEventWait; }
		}

		/// <summary>
		/// Gets the message wait.
		/// </summary>
		/// <value>The message wait.</value>
		public virtual int MessageWait
		{
			get { return DefaultMessageWait; }
		}

		/// <summary>
		///     Gets the message thread priority.
		/// </summary>
		/// <value>The message thread priority.</value>
		protected virtual ThreadPriority MessageThreadPriority
		{
			get { return DefautMessageThreadPriority; }
		}

		/// <summary>
		/// Gets the is processing messages.
		/// </summary>
		/// <value>The is processing messages.</value>
		public bool IsProcessingMessages { get; private set; }

		/// <summary>
		/// Gets a value indicating whether this instance is processing events.
		/// </summary>
		/// <value><c>true</c> if this instance is processing events; otherwise, <c>false</c>.</value>
		public bool IsProcessingEvents { get; private set; }

		/// <summary>
		///     Starts the processing.
		/// </summary>
		public override void StartProcessing()
		{
			base.StartProcessing();


			if (!IsProcessingMessages)
			{
				IsProcessingMessages = true;

				_messageProcessingThread = new Thread(ProcessMessages) {Priority = MessageThreadPriority, IsBackground = true};
				_messageProcessingThread.Start();
			}

			if (!IsProcessingEvents)
			{
				IsProcessingEvents = true;

				_eventProcessingThread = new Thread(ProcessEvents) {Priority = MessageThreadPriority, IsBackground = true};
				_eventProcessingThread.Start();
			}
		}

		/// <summary>
		///     Stops the processing.
		/// </summary>
		public override void StopProcessing()
		{
			base.StopProcessing();

			if (IsProcessingMessages)
			{
				IsProcessingMessages = false;

				if (!_messageProcessingThread.Join(MessageWait*2))
					_messageProcessingThread.Abort();

				_messageProcessingThread = null;
			}

			if (IsProcessingEvents)
			{
				IsProcessingEvents = false;

				if (!_eventProcessingThread.Join(EventWait*2))
					_eventProcessingThread.Abort();

				_eventProcessingThread = null;
			}
		}

		/// <summary>
		///     Enqueues the message.
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

		/// <summary>
		///     Pushes the command without response asynchronus.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>System.Boolean.</returns>
		protected abstract bool PushCommandWithoutResponseAsynchronous(ICommand<TIdentifier, TStore> command);

		/// <summary>
		///     Pushes the command with response asynchronous.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns>System.Boolean.</returns>
		protected abstract bool PushCommandWithResponseAsync(ICommandWithResponse<TIdentifier, TStore> commandWithResponse);

		/// <summary>
		///     Processes the commands.
		/// </summary>
		protected override void ProcessCommands()
		{
			
		}

		/// <summary>
		///     Processes the messages.
		/// </summary>
		private void ProcessMessages()
		{
			while (IsProcessingMessages)
			{
				_messageResetEvent.WaitOne(MessageWait);

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

					// TODO -- process command responses

					if (handled)
						continue;

					lock (_eventLock)
					{
						var t = HandleEvent(message, ref handled);
						if (t != null && handled)
						{
							// queue the event to fire
							_eventQueue.Enqueue(t);
						}
					}
				} while (message != null && IsProcessingMessages);
			}
		}

		/// <summary>
		///     Registers the event handler.
		/// </summary>
		/// <typeparam name="TEvt">The type of the t evt.</typeparam>
		/// <param name="key">The key.</param>
		/// <param name="action">The action.</param>
		protected void RegisterEventHandler<TEvt>(IKey<TIdentifier, TStore> key, Action<TEvt> action)
			where TEvt : class, TEvent, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		{
			_eventActions.Add(new EventAction<TIdentifier, TStore, TEvt>(action, QueueEvent, key));
		}


		/// <summary>
		///     Queues the event.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="evt">The evt.</param>
		private void QueueEvent(IEventAction<TIdentifier, TStore> action, TEvent evt)
		{
			lock (_eventQueue)
			{
				_eventQueue.Enqueue(new Tuple<IEventAction<TIdentifier, TStore>, TEvent>(action, evt));
				_eventResetEvent.Set();
			}
		}

		/// <summary>
		///     Processes the events.
		/// </summary>
		private void ProcessEvents()
		{
			while (IsProcessingEvents)
			{
				_eventResetEvent.WaitOne(EventWait);

				IEventAction<TIdentifier, TStore> eventAction = null;
				TEvent evt = null;

				lock (_eventLock)
				{
					if (_eventQueue.Any())
					{
						var item = _eventQueue.Dequeue();
						eventAction = item.Item1;
						evt = item.Item2;
					}
				}

				if (eventAction != null && evt != null)
				{
					// fire off the event
					eventAction.Invoke(evt);
				}
			}
		}

		/// <summary>
		///     Handles the event.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <param name="handled">if set to <c>true</c> [handled].</param>
		/// <returns>The handled event.</returns>
		private Tuple<IEventAction<TIdentifier, TStore>, TEvent> HandleEvent(TStore store, ref bool handled)
		{
			foreach (var action in _eventActions)
			{
				var evt = (TEvent) action.HandleEvent(store, ref handled);
				if (handled)
					return new Tuple<IEventAction<TIdentifier, TStore>, TEvent>(action, evt);
			}

			return null;
		}
	}
}