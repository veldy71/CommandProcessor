using Veldy.Net.CommandProcessor.Text;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	///     Interface ICommand
	/// </summary>
	internal interface ICommand : ICommand<MessageIdentifier>
	{
	}
}