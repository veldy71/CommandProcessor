using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Interface IEventDelegate
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TEvent"></typeparam>
	public interface IEventDelegate<TEvent, TIdentifier, TStore>
		where TEvent : class, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore> 
		where TIdentifier : struct, IConvertible
		where TStore : class
	{
		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		IKey<TIdentifier, TStore> Key { get; } 

		/// <summary>
		/// Handles the store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <returns><c>true</c> if handled, <c>false</c> otherwise.</returns>
		TEvent HandleStore(TStore store);

		/// <summary>
		/// Gets the event action for event callback.
		/// </summary>
		/// <value>The event action.</value>
		Action<EventEventArgs<TIdentifier, TStore, TEvent>> EventAction { get; }
	}
}
