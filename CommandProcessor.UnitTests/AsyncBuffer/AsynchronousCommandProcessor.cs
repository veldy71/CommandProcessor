using System;
using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	/// Class AsynchronousCommandProcessor.
	/// </summary>
	sealed class AsynchronousCommandProcessor
		: Buffer.AsynchronousCommandProcessor<Identifier, ICommand, ICommandWithResponse<Identifier, byte[], Response>, Response, Event>, 
		IAsynchronousCommandProcessor
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AsynchronousCommandProcessor"/> class.
		/// </summary>
		public AsynchronousCommandProcessor()
		{
			
		}

		/// <summary>
		/// Pushes the command without response asynchronus.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>System.Boolean.</returns>
		protected override bool PushCommandWithoutResponseAsynchronus(ICommand<Identifier, byte[]> command)
		{
			// TODO

			return false;
		}

		/// <summary>
		/// Pushes the command with response asynchronous.
		/// </summary>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <returns>System.Boolean.</returns>
		protected override bool PushCommandWithResponseAsync(ICommandWithResponse<Identifier, byte[], Response> commandWithResponse)
		{
			// simulate a response
			var store = commandWithResponse.Store;

			// response store
			var responseStore = new byte[store.Length];
			System.Buffer.BlockCopy(store, 0, responseStore, 0, store.Length);
			responseStore[1] = (byte)MessageType.ResponseType;

			// response store
			var eventStore = new byte[store.Length];
			System.Buffer.BlockCopy(store, 0, eventStore, 0, store.Length);
			eventStore[1] = (byte)MessageType.EventType;

			EnqueueMessage(responseStore);
			EnqueueMessage(eventStore);

			return false;
		}
	}
}
