using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	/// <summary>
	///     Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	internal interface ICommandWithResponse<out TResponse> : ICommandWithResponse<MessageIdentifier, TResponse>
		where TResponse : class, IResponse, IResponse<MessageIdentifier>, IResponse<MessageIdentifier, byte[]>,
			IMessage<MessageIdentifier, byte[]>
	{
	}
}