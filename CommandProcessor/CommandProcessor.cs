using System;

namespace Veldy.Net.CommandProcessor
{
    public abstract class CommandProcessor<TIdentifier, TStore, TCommandResponse, TResponse> : ICommandProcessor<TIdentifier,TStore, TCommandResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TStore : class
        where TCommandResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>
        where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
    {
    }
}
