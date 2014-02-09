namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	interface ICommandWithResponse<out TResponse> : Buffer.ICommandWithResponse<MessageIdentifier, TResponse>
		where TResponse : class, IResponse, Buffer.IResponse<MessageIdentifier>, IResponse<MessageIdentifier, byte[]>, IMessage<MessageIdentifier, byte[]>
	{
	}
}
