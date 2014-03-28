using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class AsynchronousCommandProcessor. This class cannot be inherited.
	/// </summary>
	sealed class AsynchronousCommandProcessor : AsynchronousCommandProcessor<Identifier, byte[], ICommand, ICommandWithResponse<Response>, Response, IEvent>
	{
		/// <summary>
		/// Gets the command timeout.
		/// </summary>
		/// <value>The command timeout.</value>
		public override int CommandTimeout
		{
			get { return 3600000; }
		}

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
			}

			return true;
		}
	}
}
