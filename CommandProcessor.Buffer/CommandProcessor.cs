﻿using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	public abstract class CommandProcessorCommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse> 
        : CommandProcessor<TIdentifier, byte[], TCommand, TCommandWithResponse, TResponse>, ICommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>, ICommandWithResponse<TIdentifier, byte[], TResponse>, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>, new()
    {}
}
