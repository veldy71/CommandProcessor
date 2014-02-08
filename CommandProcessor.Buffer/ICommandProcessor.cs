using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface ICommandProcessor<in TIdentifier, in TCommand, in TCommandWithResponse, in TResponse> 
        : ICommandProcessor<TIdentifier, byte[], TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>, ICommandWithResponse<TIdentifier, byte[], TResponse>, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
    { }
}
