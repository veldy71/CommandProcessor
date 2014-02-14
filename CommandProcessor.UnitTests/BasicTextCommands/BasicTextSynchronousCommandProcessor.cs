namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	/// Class BasicTextSynchronousCommandProcessor.
	/// </summary>
	class BasicTextSynchronousCommandProcessor : Text.SynchronousCommandProcessor<MessageIdentifier, ICommand, ICommandWithResponse<IResponse>, IResponse>
	{
		/// <summary>
		/// Sends the command.
		/// </summary>
		/// <typeparam name="TResponse">The type of the response.</typeparam>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		public TResponse SendCommand<TResponse>(ICommandWithResponse<TResponse> command)
			where TResponse : class, IResponse, Text.IResponse<MessageIdentifier>, IResponse<MessageIdentifier, string>,
				IMessage<MessageIdentifier, string>, new()
		{
			return SendCommand<ICommandWithResponse<TResponse>, TResponse>(command);
		}

		/// <summary>
		/// Pushes the command with response.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns></returns>
		protected override string PushCommandWithResponse(ICommandWithResponse<MessageIdentifier, string, IResponse> commandWithResponse)
		{
			// for the echo test, just return the same string for store
			return commandWithResponse.Store;
		}

		/// <summary>
		/// Pushes the command without response.
		/// </summary>
		/// <param name="command">The command.</param>
		protected override void PushCommandWithoutResponse(ICommand<MessageIdentifier, string> command)
		{
			
		}
	}
}
