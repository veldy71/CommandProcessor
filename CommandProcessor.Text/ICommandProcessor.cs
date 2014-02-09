using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public interface ICommandProcessor<in TIdentifier, in TCommand, in TCommandWithResponse, in TResponse>
        : ICommandProcessor<TIdentifier, string, TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>, ICommandWithResponse<TIdentifier, string, TResponse>, ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier, string>, new()
    { }
}
