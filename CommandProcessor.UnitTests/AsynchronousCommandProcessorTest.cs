using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer;

namespace Veldy.Net.CommandProcessor.UnitTests
{
	/// <summary>
	///     Class AsynchronousCommandProcessorTest.
	/// </summary>
	[TestClass]
	public class AsynchronousCommandProcessorTest
	{
		/// <summary>
		///     Buffers the test.
		/// </summary>
		[TestMethod]
		public void BufferTest()
		{
			var payload = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05};

			var processor = new AsynchronousCommandProcessor();
			try
			{
				bool eventSuccess = false;
				var resetEvent = new ManualResetEvent(false);

				processor.StartProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);

				processor.EchoEvent += (sender, args) =>
				{
					eventSuccess = true;
					resetEvent.Set();
				};

				var command = new EchoCommand {PayLoad = payload};
				EchoResponse response = processor.SendCommandWithResponse(command);
				Assert.IsNotNull(response, "SendCommand did not return a response.");
				Assert.IsInstanceOfType(response, typeof (EchoResponse));
				Assert.IsTrue(BufferCompare(response.Payload, payload), "Response payload did not match the command payload.");

				bool signaled = resetEvent.WaitOne(10000);
				Assert.IsTrue(signaled);
				Assert.IsTrue(eventSuccess);
			}
			finally
			{
				processor.StopProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}

		/// <summary>
		///     Texts the test.
		/// </summary>
		[TestMethod]
		public void TextTest()
		{
		}

		/// <summary>
		///     Buffers the compare.
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

			bool match = true;
			for (int i = 0; match && i < lhs.Length; i++)
				match = lhs[i] == rhs[i];

			return match;
		}
	}
}