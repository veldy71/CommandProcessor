using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Interface IEvent
	/// </summary>
	internal interface IEvent : IEvent<Identifier>, IMessage
	{
	}
}