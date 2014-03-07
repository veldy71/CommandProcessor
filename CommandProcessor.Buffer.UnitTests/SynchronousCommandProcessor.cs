using System;
using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class SynchronousCommandProcessor.
	/// </summary>
	sealed class SynchronousCommandProcessor : SynchronousCommandProcessor<Identifier, byte[], ICommand, ICommandWithResponse<Response>, Response>
	{
		/// <summary>
		/// Pushes the command with response.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns>System.Byte[][].</returns>
		protected override byte[] PushCommandWithResponse(ICommandWithResponse<Identifier, byte[]> commandWithResponse)
		{
			// return the same message that was sent as the command except change it to a response type

			var buffer = commandWithResponse.Store;

			var commandId = commandWithResponse.Key.Identifier;
			var responseId = new Identifier(commandId.MessageIdentifier, MessageType.ResponseType);

			var idBytes = BitConverter.GetBytes((ushort)responseId);

			var newBuffer = new byte[buffer.Length];

			System.Buffer.BlockCopy(idBytes, 0, newBuffer, 0, idBytes.Length);
			System.Buffer.BlockCopy(buffer, idBytes.Length, newBuffer, idBytes.Length, buffer.Length - idBytes.Length);

			return newBuffer;
		}

		/// <summary>
		/// Pushes the command without response.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		protected override void PushCommandWithoutResponse(ICommand<Identifier, byte[]> command)
		{
			throw new NotImplementedException();
		}
	}
}
