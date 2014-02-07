using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class CommandProcessor<TIdentifier, TCommand, TResponse> : CommandProcessor<TIdentifier, string, TCommand, TResponse>, ICommandProcessor<TIdentifier, TCommand, TResponse>
		where TIdentifier : struct, IConvertible
		where TCommand : class, ICommand<TIdentifier, TResponse>, ICommand<TIdentifier, string, TResponse>, IMessage<TIdentifier>, IMessage<TIdentifier, string>
		where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier>, IMessage<TIdentifier, string>, new()
    {
    }
}
