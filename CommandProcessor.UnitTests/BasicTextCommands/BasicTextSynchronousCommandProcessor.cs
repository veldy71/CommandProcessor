using Veldy.Net.CommandProcessor.Text;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	///     Class BasicTextSynchronousCommandProcessor.
	/// </summary>
	internal class BasicTextSynchronousCommandProcessor :
		SynchronousCommandProcessor<MessageIdentifier, ICommand, ICommandWithResponse<Response>, Response>
	{
		/// <summary>
		///     Pushes the command with response.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns></returns>
		protected override string PushCommandWithResponse(
			ICommandWithResponse<MessageIdentifier, string, Response> commandWithResponse)
		{
			// for the echo test, just return the same string for store
			return commandWithResponse.Store;
		}

		/// <summary>
		///     Pushes the command without response.
		/// </summary>
		/// <param name="command">The command.</param>
		protected override void PushCommandWithoutResponse(ICommand<MessageIdentifier, string> command)
		{
		}
	}
}