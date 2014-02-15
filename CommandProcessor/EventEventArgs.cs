using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class EventEventArgs.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identity.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	public class EventEventArgs<TIdentifier, TStore, TEvent> : EventArgs
		where TIdentifier : struct, IConvertible
		where TStore : class
		where TEvent : class, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		/// <summary>
		/// Gets the event.
		/// </summary>
		/// <value>The event.</value>
		public TEvent Event { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EventEventArgs{TIdentity, TStore, TEvent}"/> class.
		/// </summary>
		/// <param name="evt">The evt.</param>
		/// <exception cref="System.ArgumentNullException">evt</exception>
		public EventEventArgs(TEvent evt)
		{
			if (evt == null)
				throw new ArgumentNullException("evt");

			Event = evt;
		}
	}
}
