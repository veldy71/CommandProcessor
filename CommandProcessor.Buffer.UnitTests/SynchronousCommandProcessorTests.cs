﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandProcessor.Buffer.UnitTests
{
	[TestClass]
	public class SynchronousCommandProcessorTests
	{
		/// <summary>
		/// Basics the command test.
		/// </summary>
		[TestMethod]
		public void BasicCommandTest()
		{
			var payload = new byte[] {0x01, 0x02, 0x03, 0x04};

			var command = new EchoCommand { Payload = payload};

			var idBytes= BitConverter.GetBytes(Convert.ToUInt16(new Identifier(MessageIdentifier.Echo, MessageType.ResponseType)));

			var responseStore = new byte[idBytes.Length + payload.Length];
			System.Buffer.BlockCopy(idBytes, 0, responseStore, 0, idBytes.Length);
			System.Buffer.BlockCopy(payload, 0, responseStore, idBytes.Length, payload.Length);

			var response = command.CreateResponse(responseStore);

			Assert.IsNotNull(response);
			Assert.IsInstanceOfType(response, typeof(EchoResponse));
			Assert.IsTrue(BufferCompare(response.Payload, payload));
		}

		/// <summary>
		/// Buffers the compare.
		/// </summary>
		/// <param name="lhs">The LHS.</param>
		/// <param name="rhs">The RHS.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool BufferCompare(byte[] lhs, byte[] rhs)
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
