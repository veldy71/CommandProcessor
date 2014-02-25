using System;

namespace Veldy.Net.CommandProcessor
{
	public class EventEventArgs<TIdentifier, TStore, TEvent> : EventArgs
		where TIdentifier : struct, IConvertible
		where TStore : class
		where TEvent : class, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		/// <summary>
		/// Gets or sets the event.
		/// </summary>
		/// <value>The event.</value>
		public TEvent Event { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EventEventArgs{TIdentifier, TStore, TEvent}"/> class.
		/// </summary>
		/// <param name="evt">The evt.</param>
		public EventEventArgs(TEvent evt)
		{
			this.Event = evt;
		}
	}
}
