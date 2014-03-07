using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Interface IEvent
	/// </summary>
	interface IEvent : IEvent<Identifier, byte[]>, IMessage
	{
	}
}
