using System;
using System.Diagnostics;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class EventActionArgs.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	class EventActionArgs<TIdentifier, TStore, TEvent>
		where TIdentifier : struct, IConvertible
		where TStore : class
		where TEvent : class, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{ 
		/// <summary>
		/// Gets the event action.
		/// </summary>
		/// <value>The event action.</value>
		public IEventDelegate<TEvent, TIdentifier, TStore> EventDelegate { get; private set; }

		/// <summary>
		/// Gets the event arguments.
		/// </summary>
		/// <value>The event arguments.</value>
		public EventEventArgs<TIdentifier, TStore, TEvent> EventEventArgs { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EventActionArgs{TIdentifier, TStore, TEvent}"/> class.
		/// </summary>
		/// <param name="eventDelegate">The event delegate.</param>
		/// <param name="eventEventEventEventArgs">The event event event event arguments.</param>
		/// <exception cref="System.ArgumentNullException">
		/// eventDelegate
		/// or
		/// eventEventEventEventArgs
		/// </exception>
		public EventActionArgs(IEventDelegate<TEvent, TIdentifier, TStore> eventDelegate,
			EventEventArgs<TIdentifier, TStore, TEvent> eventEventEventEventArgs)
		{
			if (eventDelegate == null)
				throw new ArgumentNullException("eventDelegate");

			if (eventEventEventEventArgs == null)
				throw new ArgumentNullException("eventEventEventEventArgs");

			EventDelegate = eventDelegate;
			EventEventArgs = eventEventEventEventArgs;
		}

		/// <summary>
		/// Invokes this instance.
		/// </summary>
		public void Invoke()
		{
			Debug.Assert(EventEventArgs != null);
			EventDelegate.EventAction(EventEventArgs);
		}
	}
}
