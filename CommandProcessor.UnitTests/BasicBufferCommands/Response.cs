﻿namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	abstract class Response : Buffer.Response<MessageIdentifier>, IResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Response"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected Response(MessageIdentifier identifier) 
			: base(identifier)
		{
		}
	}
}
