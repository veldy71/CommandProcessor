using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	public abstract class CommandProcessor<TIdentifier, TCommandResponse, TResponse> 
        : CommandProcessor<TIdentifier, byte[], TCommandResponse, TResponse>, ICommandProcessor<TIdentifier, TCommandResponse, TResponse> 
        where TIdentifier : struct, IConvertible
        where TCommandResponse : class, ICommandWithResponse<TIdentifier, TResponse>, ICommandWithResponse<TIdentifier, byte[], TResponse>, ICommand<TIdentifier, byte[]>
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>, new()
    { }
}
