using Veldy.Net.CommandProcessor.Text;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	///     Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	internal interface ICommandWithResponse<out TResponse> : ICommandWithResponse<MessageIdentifier, TResponse>
		where TResponse : class, IResponse, IResponse<MessageIdentifier>, IResponse<MessageIdentifier, string>,
			IMessage<MessageIdentifier, string>
	{
	}
}