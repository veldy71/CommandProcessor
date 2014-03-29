using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandProcessor.Buffer.UnitTests
{
	[TestClass]
	public class AsynchronousCommandProcessorTests
	{
		/// <summary>
		/// Asynchronous the command processor test.
		/// </summary>
		[TestMethod]
		public void AsynchronousCommandProcessorTest()
		{
			var payload = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A };

			var commandProcessor = new AsynchronousCommandProcessor();

			try
			{
				Assert.IsFalse(commandProcessor.IsProcessingCommands);
				Assert.IsFalse(commandProcessor.IsProcessingMessages);
				Assert.IsFalse(commandProcessor.IsProcessingEvents);
				commandProcessor.StartProcessing();
				Assert.IsTrue(commandProcessor.IsProcessingCommands);
				Assert.IsTrue(commandProcessor.IsProcessingMessages);
				Assert.IsTrue(commandProcessor.IsProcessingEvents);

				var echoResponse = commandProcessor.SendCommandWithResponse(new EchoCommand { Payload = payload });
				Assert.IsNotNull(echoResponse);
				Assert.IsInstanceOfType(echoResponse, typeof(EchoResponse));

				Assert.IsTrue(BufferCompare(payload, echoResponse.Payload));
			}
			finally
			{
				Assert.IsTrue(commandProcessor.IsProcessingCommands);
				Assert.IsTrue(commandProcessor.IsProcessingMessages);
				Assert.IsTrue(commandProcessor.IsProcessingEvents);
				commandProcessor.StopProcessing();
				Assert.IsFalse(commandProcessor.IsProcessingCommands);
				Assert.IsFalse(commandProcessor.IsProcessingMessages);
				Assert.IsFalse(commandProcessor.IsProcessingEvents);
			}
		}

		/// <summary>
		/// Buffers the compare.
		/// </summary>
		/// <param name="lhs">The LHS.</param>
		/// <param name="rhs">The RHS.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private static bool BufferCompare(byte[] lhs, byte[] rhs)
		{
			if (lhs == null && rhs == null)
				return true;

			if (lhs == null || rhs == null)
				return false;

			if (lhs.Length != rhs.Length)
				return false;

			var e = true;
			for (var i = 0; e && i < lhs.Length; i++)
				e = lhs[i] == rhs[i];

			return e;
		}
	}
}
