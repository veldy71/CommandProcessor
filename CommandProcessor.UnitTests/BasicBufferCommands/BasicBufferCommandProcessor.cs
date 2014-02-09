using System.Net.Configuration;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	class BasicBufferCommandProcessor : Buffer.CommandProcessor<MessageIdentifier, ICommand, ICommandWithResponse<IResponse>, IResponse>
	{
		/// <summary>
		/// Pushes the command with no response.
		/// </summary>
		/// <param name="store">The store.</param>
		protected override void PushCommandWithNoResponse(byte[] store)
		{
			
		}

		/// <summary>
		/// Pushes the command with response.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <param name="responseLength">Length of the response.</param>
		/// <returns></returns>
		protected override byte[] PushCommandWithResponse(byte[] store, int responseLength)
		{
			// for the echo command, just return the same store
			return store;
		}

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
	}
}
