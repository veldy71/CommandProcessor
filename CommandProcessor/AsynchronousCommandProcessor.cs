using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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

		private readonly ConcurrentQueue<Tuple<IEventAction<TIdentifier, TStore>, TEvent>> _eventQueue =
			new ConcurrentQueue<Tuple<IEventAction<TIdentifier, TStore>, TEvent>>();

		private readonly AutoResetEvent _eventResetEvent = new AutoResetEvent(false);

		private readonly ConcurrentQueue<TStore> _messageQueue = new ConcurrentQueue<TStore>();
		private readonly AutoResetEvent _messageResetEvent = new AutoResetEvent(false);

		private Thread _eventProcessingThread;
		private Thread _messageProcessingThread;

		private const ThreadPriority DefautMessageThreadPriority = ThreadPriority.Normal;

		private const int DefaultMessageWait = 1000;
		private const int DefaultEventWait = 1000;

		private ICommandWithResponseTransaction<TIdentifier, TStore, ICommandWithResponse<TIdentifier, TStore>>
			_currentCommandWithResponseTransaction = null;

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

			_messageQueue.Enqueue(store);
			_messageResetEvent.Set();
		}

		/// <summary>
		///     Pushes the command without response asynchronus.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>System.Boolean.</returns>
		protected abstract bool PushCommandWithoutResponseAsync(ICommand<TIdentifier, TStore> command);

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
			while (IsProcessingCommands)
			{
				_ProcessCommandsResetEvent.WaitOne(CommandWait);

				while (_CommandQueue.Any())
				{
					// waiting on a response the previous transaction
					lock (_TransactionLock)
					{
						if (_currentCommandWithResponseTransaction != null)
							break;

						ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>> commandTransaction;

						if (!_CommandQueue.TryDequeue(out commandTransaction))
							continue;

						if (commandTransaction.HasResponse)
						{
							_currentCommandWithResponseTransaction =
								(ICommandWithResponseTransaction<TIdentifier, TStore, ICommandWithResponse<TIdentifier, TStore>>)
									commandTransaction;

							if (PushCommandWithResponseAsync(_currentCommandWithResponseTransaction.CommandWithResponse))
								_messageResetEvent.Set();
							else
							{
								_currentCommandWithResponseTransaction.SetException(new IOException("Failed to push command for response."));
								_currentCommandWithResponseTransaction = null;
							}
						}
						else
						{
							if (PushCommandWithoutResponseAsync(commandTransaction.Command))
								commandTransaction.SetInactive();
							else
								commandTransaction.SetException(new IOException("Failed to push command without response."));
						}
					}
				}
			}
		}

		/// <summary>
		///     Processes the messages.
		/// </summary>
		private void ProcessMessages()
		{
			while (IsProcessingMessages)
			{
				_messageResetEvent.WaitOne(MessageWait);

				while (_messageQueue.Any())
				{
					TStore message;
					if (!_messageQueue.TryDequeue(out message))
						continue;

					var handled = false;

					lock (_TransactionLock)
					{
						if (_currentCommandWithResponseTransaction != null
						    && _currentCommandWithResponseTransaction.IsActive
						    && _currentCommandWithResponseTransaction.SetResponseStore(message))
						{
							handled = true;
							_currentCommandWithResponseTransaction = null;
						}
					}

					if (!handled)
						handled = HandleEvent(message);

					if (!handled)
						throw new MessageNotHandledException<TStore>(message);

				}
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
			_eventQueue.Enqueue(new Tuple<IEventAction<TIdentifier, TStore>, TEvent>(action, evt));
			_eventResetEvent.Set();
		}

		/// <summary>
		///     Processes the events.
		/// </summary>
		private void ProcessEvents()
		{
			while (IsProcessingEvents)
			{
				_eventResetEvent.WaitOne(EventWait);

				while (_eventQueue.Any())
				{
					Tuple<IEventAction<TIdentifier, TStore>, TEvent> tuple;

					if (!_eventQueue.TryDequeue(out tuple))
						continue;

					var eventAction = tuple.Item1;
					var evt = tuple.Item2;

					// fire off the event
					eventAction.Invoke(evt);
				}
			}
		}

		/// <summary>
		/// Handles the event.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <returns>The handled event.</returns>
		private bool HandleEvent(TStore store)
		{
			var handled = false;
			return _eventActions.Select(action => (TEvent) action.HandleEvent(store, ref handled)).Any(evt => handled);
		}
	}
}