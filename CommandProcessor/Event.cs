using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class Event.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	public class Event<TIdentifier, TStore> : IEvent<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible
		where TStore : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Event{TIdentifier, TStore}"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		/// <param name="key">The key.</param>
		protected Event(TIdentifier identifier, IKey<TIdentifier, TStore> key)
		{
			if (key == null)
				throw new ArgumentNullException("key");

			Key = Key;
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		public IKey<TIdentifier, TStore> Key { get; protected set; }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public TStore Store { get; private set; }

		/// <summary>
		/// Sets the store.
		/// </summary>
		/// <param name="store">The store.</param>
		public void SetStore(TStore store)
		{
			Store = store;
		}
	}
}
