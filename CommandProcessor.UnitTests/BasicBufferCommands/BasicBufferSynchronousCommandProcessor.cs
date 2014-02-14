namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	/// <summary>
	/// Class BasicBufferSynchronousCommandProcessor.
	/// </summary>
	class BasicBufferSynchronousCommandProcessor : Buffer.SynchronousCommandProcessor<MessageIdentifier, ICommand, ICommandWithResponse<IResponse>, IResponse>
	{
		/// <summary>
		/// Sends the command.
		/// </summary>
		/// <typeparam name="TResponse">The type of the response.</typeparam>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		public TResponse SendCommand<TResponse>(ICommandWithResponse<TResponse> command)
			where TResponse : class, IResponse, Buffer.IResponse<MessageIdentifier>, IResponse<MessageIdentifier, byte[]>,
				IMessage<MessageIdentifier, byte[]>, new()
		{
			return SendCommand<ICommandWithResponse<TResponse>, TResponse>(command);
		}

		/// <summary>
		/// Pushes the command with response.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns></returns>
		protected override byte[] PushCommandWithResponse(ICommandWithResponse<MessageIdentifier, byte[], IResponse> commandWithResponse)
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
