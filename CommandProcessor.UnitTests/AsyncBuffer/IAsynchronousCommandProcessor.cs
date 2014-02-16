﻿namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	/// Interface IAsynchronousCommandProcessor
	/// </summary>
	interface IAsynchronousCommandProcessor 
		: Buffer.IAsynchronousCommandProcessor<MessageIdentifier, ICommand, ICommandWithResponse<IResponse>, IResponse, IEvent>
	{
	}
}
