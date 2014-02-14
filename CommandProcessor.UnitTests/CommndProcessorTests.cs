using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands;
using Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands;

namespace Veldy.Net.CommandProcessor.UnitTests
{
	/// <summary>
	/// Class CommandProcessorTests.
	/// </summary>
	[TestClass]
	public class CommandProcessorTests
	{
		/// <summary>
		/// Basics the buffer start stop command processor test.
		/// </summary>
		[TestMethod]
		public void BasicBufferStartStopCommandProcessorTest()
		{
			var processor = new BasicBufferSynchronousCommandProcessor();
			try
			{
				processor.StartProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);
			}
			finally
			{
				processor.StopProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}

		/// <summary>
		/// Basics the text start stop command processor test.
		/// </summary>
		[TestMethod]
		public void BasicTextStartStopCommandProcessorTest()
		{
			var processor = new BasicTextSynchronousCommandProcessor();
			try
			{
				processor.StartProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);
			}
			finally
			{
				processor.StopProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}
	}
}
