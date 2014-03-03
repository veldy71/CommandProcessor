using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands;
using Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands;
using EchoCommand = Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands.EchoCommand;
using EchoResponse = Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands.EchoResponse;

namespace Veldy.Net.CommandProcessor.UnitTests
{
	/// <summary>
	///     Class CommandProcessorSendCommandTests.
	/// </summary>
	[TestClass]
	public class CommandProcessorSendCommandTests
	{
		/// <summary>
		///     Buffers the echo command test.
		/// </summary>
		[TestMethod]
		public void BufferEchoCommandTest()
		{
			var payload = new byte[] {0x01, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05};

			var processor = new BasicBufferSynchronousCommandProcessor();
			try
			{
				processor.StartProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);

				var command = new EchoCommand {PayLoad = payload};
				EchoResponse response = processor.SendCommandWithResponse(command);
				Assert.IsNotNull(response, "SendCommand did not return a response.");
				Assert.IsInstanceOfType(response, typeof (EchoResponse));
				Assert.IsTrue(BufferCompare(response.Payload, payload), "Response payload did not match the command payload.");
			}
			finally
			{
				processor.StopProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}

		/// <summary>
		///     Texts the echo command test.
		/// </summary>
		[TestMethod]
		public void TextEchoCommandTest()
		{
			var payload = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05};

			var processor = new BasicTextSynchronousCommandProcessor();
			try
			{
				processor.StartProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);

				var command = new BasicTextCommands.EchoCommand {Payload = payload};
				BasicTextCommands.EchoResponse response = processor.SendCommandWithResponse(command);
				Assert.IsNotNull(response, "SendCommand did not return a response.");
				Assert.IsInstanceOfType(response, typeof (BasicTextCommands.EchoResponse));
				Assert.IsTrue(BufferCompare(response.Payload, payload), "Response payload did not match the command payload.");
			}
			finally
			{
				processor.StopProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}

		/// <summary>
		///     Buffers the compare.
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

			bool match = true;
			for (int i = 0; match && i < lhs.Length; i++)
				match = lhs[i] == rhs[i];

			return match;
		}
	}
}