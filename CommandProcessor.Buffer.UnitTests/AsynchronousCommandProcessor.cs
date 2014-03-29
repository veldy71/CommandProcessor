using System;
using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class AsynchronousCommandProcessor. This class cannot be inherited.
	/// </summary>
	sealed class AsynchronousCommandProcessor : AsynchronousCommandProcessor<Identifier, byte[], ICommand, ICommandWithResponse<Response>, Response, IEvent>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AsynchronousCommandProcessor"/> class.
		/// </summary>
		public AsynchronousCommandProcessor()
		{
			RegisterEventHandler(new Key(new Identifier(MessageIdentifier.Echo, MessageType.EventType)), new Action<EchoEvent>(OnEcho));
		}

		/// <summary>
		/// Called when [echo].
		/// </summary>
		/// <param name="echoEvent">The echo event.</param>
		private void OnEcho(EchoEvent echoEvent)
		{
			if (Echo != null)
				Echo(this, new EchoEventArgs(echoEvent.Payload));
		}

		/// <summary>
		/// Occurs when [echo].
		/// </summary>
		public event EventHandler<EchoEventArgs> Echo;

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
			// take the command and insert a response to it
			if (commandWithResponse is EchoCommand)
			{
				// create an echo response and enqueue it
				var echoCommand = (EchoCommand) commandWithResponse;

				var echoResponse = echoCommand.CreateResponse();
				echoResponse.SetPayload(echoCommand.Payload);

				EnqueueMessage(echoResponse.Store);

				// create an echo event from the payload of the echo command.
				var echoEvent = new EchoEvent();
				var id = BitConverter.GetBytes(Convert.ToUInt16(echoEvent.Key.Identifier));
				var eventStore = echoCommand.Store;
				System.Buffer.BlockCopy(id, 0, eventStore, 0, id.Length);

				EnqueueMessage(eventStore);
			}

			return true;
		}
	}
}
