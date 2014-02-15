using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Class Event.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public abstract class Event<TIdentifier> : Event<TIdentifier, byte[]>, IEvent<TIdentifier> 
		where TIdentifier : struct, IConvertible
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Event{TIdentifier, TStore}"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected Event(TIdentifier identifier)
 			: base(identifier, new Key<TIdentifier>(identifier))
		{ }
	}
}
