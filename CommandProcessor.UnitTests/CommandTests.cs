using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands;
using Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands;
using EchoResponse = Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands.EchoResponse;
using MessageIdentifier = Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands.MessageIdentifier;

namespace Veldy.Net.CommandProcessor.UnitTests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void BasicCommandBufferTest()
        {
            var payload = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05};

            var commandStore = new byte[] { (byte)MessageIdentifier.Echo, 0x01, 0x02, 0x03, 0x04, 0x05 };
            var responseStore = new byte[] { (byte)MessageIdentifier.Echo, 0x01, 0x02, 0x03, 0x04, 0x05 };

            var command = new EchoCommand { PayLoad = payload };
            var store = command.Store;
            Assert.IsTrue(BufferCompare(store, commandStore), "EchoCommand.Execute() creates invalid store.");

            var response = command.CreateResponse(responseStore);
            Assert.IsNotNull(response, "EchoCommand.CreateResponse() did not create a response.");
            Assert.IsInstanceOfType(response, typeof(EchoResponse), "EchoCommand.CreateResponse() did not create a response of type EchoResponse.");
            Assert.IsTrue(BufferCompare(response.Payload, payload), "EchoResponse.Payload doesn't match payload.");
        }

        [TestMethod]
        public void BasicCommandTextTest()
        {
            var payload = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };

            var commandStore = "ECHO " + Convert.ToBase64String(payload);
            var responseStore = "ECHO " + Convert.ToBase64String(payload);

            var command = new EchoComand {Payload = payload};
            var store = command.Store;
            Assert.IsTrue(store == commandStore, "EchoCommand.Execute() creates invalid store.");

            var response = command.CreateResponse(responseStore);
            Assert.IsNotNull(response, "EchoCommand.CreateResponse() did not create a response.");
            Assert.IsInstanceOfType(response, typeof(BasicTextCommands.EchoResponse), "EchoCommand.CreateResponse() did not create a response of type EchoResponse.");
            Assert.IsTrue(BufferCompare(response.Payload, payload), "EchoResponse.Payload doesn't match payload.");
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
