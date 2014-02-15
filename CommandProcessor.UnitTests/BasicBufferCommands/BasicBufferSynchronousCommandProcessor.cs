namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	/// <summary>
	/// Class BasicBufferSynchronousCommandProcessor.
	/// </summary>
	class BasicBufferSynchronousCommandProcessor : Buffer.SynchronousCommandProcessor<MessageIdentifier, ICommand, ICommandWithResponse<Response>, Response>
	{
		/// <summary>
		/// Pushes the command with response.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns></returns>
		protected override byte[] PushCommandWithResponse(ICommandWithResponse<MessageIdentifier, byte[], Response> commandWithResponse)
		{
			// for the echo command, just return the same store
			return commandWithResponse.Store;
		}

		/// <summary>
		/// Pushes the command without response.
		/// </summary>
		/// <param name="command">The command.</param>
		protected override void PushCommandWithoutResponse(ICommand<MessageIdentifier, byte[]> command)
		{
			// nothing
		}
	}
}
