using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	internal interface ICommandWithResponse<out TResponse> : ICommandWithResponse<Identifier, TResponse>, ICommand
		where TResponse : class, IResponse, IResponse<Identifier>, IResponse<Identifier, byte[]>, IMessage,
			IMessage<Identifier, byte[]>
	{
	}
}