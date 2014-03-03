using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Class Event.
	/// </summary>
	internal class Event : Event<Identifier>, IEvent
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Event" /> class.
		/// </summary>
		public Event()
			: base(default(Identifier))
		{
		}
	}
}