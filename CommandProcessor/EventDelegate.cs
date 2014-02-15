using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class EventDelegate.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identity.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TEvent"></typeparam>
	public class EventDelegate<TEvent, TIdentifier, TStore> : IEventDelegate<TEvent, TIdentifier, TStore>
		where TEvent : class, IEvent<TIdentifier, TStore>, new()
		where TIdentifier : struct, IConvertible
		where TStore : class
	{
		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		public IKey<TIdentifier, TStore> Key { get; private set; }

		/// <summary>
		/// Handles the store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <returns><c>true</c> if handled, <c>false</c> otherwise.</returns>
		public virtual TEvent HandleStore(TStore store)
		{
			if (this.Key.CompareTo(store) == 0)
			{
				var evt = new TEvent();
				evt.SetStore(store);
				return evt;
			}

			return null;
		}

		/// <summary>
		/// Gets the event action fr event callback.
		/// </summary>
		/// <value>The event action.</value>
		public Action<EventEventArgs<TIdentifier, TStore, TEvent>> EventAction { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EventDelegate{TEvent, TIdentifier, TStore}"/> class.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="eventAction">The event action.</param>
		/// <exception cref="System.ArgumentNullException">
		/// key
		/// or
		/// eventAction
		/// </exception>
		public EventDelegate(IKey<TIdentifier, TStore> key, Action<EventEventArgs<TIdentifier, TStore, TEvent>> eventAction)
		{
			if (key == null)
				throw new ArgumentNullException("key");

			if (eventAction == null)
				throw new ArgumentNullException("eventAction");

			Key = key;
			EventAction = eventAction;
		}
	}
}
