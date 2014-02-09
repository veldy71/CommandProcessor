namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	class BasicTextCommandProcessor : Text.CommandProcessor<MessageIdentifier, ICommand, ICommandWithResponse<IResponse>, IResponse>
	{
		/// <summary>
		/// Pushes the command with no response.
		/// </summary>
		/// <param name="store">The store.</param>
		protected override void PushCommandWithNoResponse(string store)
		{
			
		}

		/// <summary>
		/// Pushes the command with response.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <param name="responseLength">Length of the response.</param>
		/// <returns></returns>
		protected override string PushCommandWithResponse(string store, int responseLength)
		{
			// for the echo test, just return the same string for store
			return store;
		}

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
	}
}
