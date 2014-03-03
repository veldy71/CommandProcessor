using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	/// <summary>
	///     Interface ICommand
	/// </summary>
	internal interface ICommand : ICommand<MessageIdentifier>
	{
	}
}