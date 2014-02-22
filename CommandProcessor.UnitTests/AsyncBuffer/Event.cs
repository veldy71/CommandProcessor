namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	/// Class Event.
	/// </summary>
	class Event : Buffer.Event<Identifier>, IEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Event" /> class.
		/// </summary>
		public Event()
			: base(default(Identifier))
		{
		}
	}
}
