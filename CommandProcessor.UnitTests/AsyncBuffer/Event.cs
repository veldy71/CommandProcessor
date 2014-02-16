namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	/// Class Event.
	/// </summary>
	abstract class Event : Buffer.Event<MessageIdentifier>, IEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Event"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected Event(MessageIdentifier identifier) 
			: base(identifier)
		{
		}
	}
}
