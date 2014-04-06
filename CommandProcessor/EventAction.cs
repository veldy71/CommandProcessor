using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Class EventAction.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	internal class EventAction<TIdentifier, TStore, TEvent> : IEventAction<TIdentifier, TStore>
		where TEvent : class, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
		private readonly Action<TEvent> _action;
		private readonly IKey<TIdentifier, TStore> _key;
		private readonly Action<IEventAction<TIdentifier, TStore>, TEvent> _queueEvent;

		/// <summary>
		///     Initializes a new instance of the <see cref="EventAction{TIdentifier, TStore, TEvent}" /> class.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="queueEvent">The queue event.</param>
		/// <param name="key">The key.</param>
		/// <exception cref="System.ArgumentNullException">
		///     @action
		///     or
		///     @key
		///     or
		///     @queueEvent
		/// </exception>
		public EventAction(Action<TEvent> action, Action<IEventAction<TIdentifier, TStore>, TEvent> queueEvent,
			IKey<TIdentifier, TStore> key)
		{
			if (action == null)
				throw new ArgumentNullException(@"action");

			if (key == null)
				throw new ArgumentNullException(@"key");

			if (queueEvent == null)
				throw new ArgumentNullException(@"queueEvent");

			_action = action;
			_queueEvent = queueEvent;
			_key = key;
		}

		/// <summary>
		///     Handles the event.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <param name="handled">if set to <c>true</c> [handled].</param>
		/// <returns>IEvent{`0`1}.</returns>
		public IEvent<TIdentifier, TStore> HandleEvent(TStore store, ref bool handled)
		{
			if (!handled)
			{
				if (_key.CompareTo(store) == 0)
				{
					var eventByType = new TEvent();
					eventByType.SetStore(store);

					handled = true;

					_queueEvent(this, eventByType);

					return eventByType;
				}

				return null;
			}

			return null;
		}

		/// <summary>
		///     Invokes the specified evt.
		/// </summary>
		/// <param name="evt">The evt.</param>
		public void Invoke(IEvent<TIdentifier, TStore> evt)
		{
			_action((TEvent) evt);
		}
	}
}