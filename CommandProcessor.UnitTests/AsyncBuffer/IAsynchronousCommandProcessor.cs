using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Interface IAsynchronousCommandProcessor
	/// </summary>
	internal interface IAsynchronousCommandProcessor
		: IAsynchronousCommandProcessor<Identifier, ICommand, ICommandWithResponse<IResponse>, IResponse, IEvent>
	{
	}
}