using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Interface IResponse
	/// </summary>
	internal interface IResponse : IResponse<Identifier>, IMessage
	{
	}
}