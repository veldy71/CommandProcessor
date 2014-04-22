using System;
using System.Diagnostics;
using System.Threading;
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
			const int iterations = 10;
			var eventsFired = 0;
			var responsePayloadsMatched = 0;
			var eventPayloadsMatched = 0;

			var random = new Random();

			var commandProcessor = new AsynchronousCommandProcessor();
			var echoResetEvent = new ManualResetEvent(false);
			byte[] payload = null;
			int payloadLength = 0;

			commandProcessor.Echo += (sender, args) =>
			{
				if (payloadLength == args.Payload.Length && BufferCompare(payload, args.Payload))
					Interlocked.Increment(ref eventPayloadsMatched);

				Interlocked.Increment(ref eventsFired);
				Debug.WriteLine("event fired #" + eventsFired);
				echoResetEvent.Set();
			};

			try
			{
				Assert.IsFalse(commandProcessor.IsProcessingCommands);
				Assert.IsFalse(commandProcessor.IsProcessingMessages);
				Assert.IsFalse(commandProcessor.IsProcessingEvents);
				commandProcessor.StartProcessing();
				Assert.IsTrue(commandProcessor.IsProcessingCommands);
				Assert.IsTrue(commandProcessor.IsProcessingMessages);
				Assert.IsTrue(commandProcessor.IsProcessingEvents);

				for (var iteration = 0; iteration < iterations; iteration++)
				{
					payloadLength = random.Next(1, 255);

					payload = new byte[payloadLength];
					random.NextBytes(payload);

					var echoResponse = commandProcessor.SendCommandWithResponse(new EchoCommand {Payload = payload});
					Debug.WriteLine("Response #" + (iteration + 1));
					Assert.IsNotNull(echoResponse);
					Assert.IsInstanceOfType(echoResponse, typeof (EchoResponse));
					if (payloadLength == echoResponse.Payload.Length && BufferCompare(payload, echoResponse.Payload))
						responsePayloadsMatched++;

					echoResetEvent.WaitOne(commandProcessor.CommandTimeout + commandProcessor.EventWait);
				}

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

				Debug.WriteLine("Iterations = " + iterations);
				Debug.WriteLine("Events fired = " + eventsFired);
				Debug.WriteLine("Events payloads matched = " + eventPayloadsMatched);
				Debug.WriteLine("Response payloads matched = " + responsePayloadsMatched);

				Assert.IsTrue(eventsFired == iterations);
				Assert.IsTrue(eventPayloadsMatched == iterations);
				Assert.IsTrue(responsePayloadsMatched == iterations);
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
