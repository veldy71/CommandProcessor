namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	/// Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	interface ICommandWithResponse<out TResponse> : Text.ICommandWithResponse<MessageIdentifier, TResponse>
		where TResponse : class, IResponse, Text.IResponse<MessageIdentifier>, IResponse<MessageIdentifier, string>, IMessage<MessageIdentifier, string>
	{
	}
}
