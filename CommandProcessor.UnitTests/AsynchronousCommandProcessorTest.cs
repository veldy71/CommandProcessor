using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Veldy.Net.CommandProcessor.UnitTests
{
	/// <summary>
	/// Class AsynchronousCommandProcessorTest.
	/// </summary>
	[TestClass]
	public class AsynchronousCommandProcessorTest
	{
		/// <summary>
		/// Buffers the test.
		/// </summary>
		[TestMethod]
		public void BufferTest()
		{
			var payload = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };

			var processor = new AsyncBuffer.AsynchronousCommandProcessor();
			try
			{
				processor.StartProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);

				var command = new AsyncBuffer.EchoCommand { PayLoad = payload };
				var response = processor.SendCommandWithResponse(command);
				Assert.IsNotNull(response, "SendCommand did not return a response.");
				Assert.IsInstanceOfType(response, typeof(AsyncBuffer.EchoResponse));
				Assert.IsTrue(BufferCompare(response.Payload, payload), "Response payload did not match the command payload.");
			}
			finally
			{
				processor.StopProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}

		/// <summary>
		/// Texts the test.
		/// </summary>
		[TestMethod]
		public void TextTest()
		{

		}

		/// <summary>
		/// Buffers the compare.
		/// </summary>
		/// <param name="lhs">The LHS.</param>
		/// <param name="rhs">The RHS.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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
