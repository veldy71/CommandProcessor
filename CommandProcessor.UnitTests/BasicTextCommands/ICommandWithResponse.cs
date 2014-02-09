namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	interface ICommandWithResponse<out TResponse> : Text.ICommandWithResponse<MessageIdentifier, TResponse>
		where TResponse : class, IResponse, Text.IResponse<MessageIdentifier>, IResponse<MessageIdentifier, string>, IMessage<MessageIdentifier, string>
	{
	}
}
