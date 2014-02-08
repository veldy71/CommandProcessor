using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface ICommandProcessor<TIdentifier, TCommandWithResponse, TResponse> : ICommandProcessor<TIdentifier, byte[], TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, byte[], TResponse>, ICommand<TIdentifier, byte[]> 
		where TResponse : class, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
    { }
}
