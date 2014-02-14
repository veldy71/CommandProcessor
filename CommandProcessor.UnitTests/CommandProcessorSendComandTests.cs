using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Veldy.Net.CommandProcessor.UnitTests
{
	/// <summary>
	/// Class CommandProcessorSendCommandTests.
	/// </summary>
	[TestClass]
	public class CommandProcessorSendCommandTests
	{
		/// <summary>
		/// Buffers the echo command test.
		/// </summary>
		[TestMethod]
		public void BufferEchoCommandTest()
		{
			var payload = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };

			var processor = new BasicBufferCommands.BasicBufferSynchronousCommandProcessor();
			try
			{
				processor.StartCommandProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);

				var command = new BasicBufferCommands.EchoCommand { PayLoad = payload };
				var response = processor.SendCommand(command);
				Assert.IsNotNull(response, "SendCommand did not return a response.");
				Assert.IsInstanceOfType(response, typeof(BasicBufferCommands.EchoResponse));
				Assert.IsTrue(BufferCompare(response.Payload, payload), "Response payload did not match the command payload.");
			}
			finally
			{
				processor.StopCommandProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}

		/// <summary>
		/// Texts the echo command test.
		/// </summary>
		[TestMethod]
		public void TextEchoCommandTest()
		{
			var payload = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };

			var processor = new BasicTextCommands.BasicTextSynchronousCommandProcessor();
			try
			{
				processor.StartCommandProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);

				var command = new BasicTextCommands.ByteBufferEchoCommand { Payload = payload };
				var response = processor.SendCommand(command);
				Assert.IsNotNull(response, "SendCommand did not return a response.");
				Assert.IsInstanceOfType(response, typeof(BasicTextCommands.EchoResponse));
				Assert.IsTrue(BufferCompare(response.Payload, payload), "Response payload did not match the command payload.");
			}
			finally
			{
				processor.StopCommandProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}

		/// <summary>
		/// Buffers the compare.
		/// </summary>
		/// <param name="lhs">The LHS.</param>
		/// <param name="rhs">The RHS.</param>
		/// <returns></returns>
		private static bool BufferCompare(byte[] lhs, byte[] rhs)
		{
			if (lhs == null || rhs == null)
				return false;

			if (lhs.Length != rhs.Length)
				return false;

			var match = true;
			for (var i = 0; match && i < lhs.Length; i++)
				match = lhs[i] == rhs[i];

			return match;
		}
	}
}
