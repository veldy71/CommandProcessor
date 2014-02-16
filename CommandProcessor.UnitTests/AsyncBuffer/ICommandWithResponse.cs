using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	/// Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	interface ICommandWithResponse<out TResponse> : Buffer.ICommandWithResponse<MessageIdentifier, TResponse>
		where TResponse : class, IResponse, IResponse<MessageIdentifier>, IResponse<MessageIdentifier, byte[]>, IMessage<MessageIdentifier, byte[]>
	{
	}
}
