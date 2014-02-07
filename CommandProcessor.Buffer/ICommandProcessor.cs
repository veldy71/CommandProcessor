using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface ICommandProcessor<TIdentifier, TCommand, TResponse> : ICommandProcessor<TIdentifier, byte[], TCommand, TResponse>
		where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier, byte[], TResponse>
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier>, IMessage<TIdentifier, byte[]>
    {
    }
}
