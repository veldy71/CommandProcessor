using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class CommandProcessor<TIdentifier, TCommandResponse, TResponse>
        : CommandProcessor<TIdentifier, string, TCommandResponse, TResponse>, ICommandProcessor<TIdentifier, TCommandResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommandResponse : class, ICommandWithResponse<TIdentifier, TResponse>, ICommandWithResponse<TIdentifier, string, TResponse>, ICommand<TIdentifier, string>
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier, string>, new()
    { }
}
