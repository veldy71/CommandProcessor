using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Interface ICommand
	/// </summary>
	internal interface ICommand : ICommand<Identifier>, IMessage
	{
	}
}