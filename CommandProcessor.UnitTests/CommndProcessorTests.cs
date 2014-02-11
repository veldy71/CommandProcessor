using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands;
using Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands;

namespace Veldy.Net.CommandProcessor.UnitTests
{
	[TestClass]
	public class CommndProcessorTests
	{
		[TestMethod]
		public void BasicBufferStartStopCommandProcessorTest()
		{
			var processor = new BasicBufferSynchronousCommandProcessor();
			try
			{
				processor.StartCommandProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);
			}
			finally
			{
				processor.StopCommandProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}

		[TestMethod]
		public void BasicTextStartStopCommandProcessorTest()
		{
			var processor = new BasicTextSynchronousCommandProcessor();
			try
			{
				processor.StartCommandProcessing();
				Assert.IsTrue(processor.IsProcessingMessages);
			}
			finally
			{
				processor.StopCommandProcessing();
				Assert.IsFalse(processor.IsProcessingMessages);
			}
		}
	}
}
