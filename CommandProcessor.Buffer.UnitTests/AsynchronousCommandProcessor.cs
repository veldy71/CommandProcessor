using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class AsynchronousCommandProcessor. This class cannot be inherited.
	/// </summary>
	sealed class AsynchronousCommandProcessor : AsynchronousCommandProcessor<Identifier, byte[], ICommand, ICommandWithResponse<Response>, Response, IEvent>
	{
		/// <summary>
		/// Pushes the command without response asynchronous.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		protected override bool PushCommandWithoutResponseAsynchronous(ICommand<Identifier, byte[]> command)
		{
			// TODO
			return true;
		}

		/// <summary>
		/// Pushes the command with response asynchronous.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		protected override bool PushCommandWithResponseAsync(ICommandWithResponse<Identifier, byte[]> commandWithResponse)
		{
			// TODO 
			return true;
		}
	}
}
