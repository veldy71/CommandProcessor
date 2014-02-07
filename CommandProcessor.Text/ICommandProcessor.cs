using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public interface ICommandProcessor<TIdentifier, TCommand, TResponse> : ICommandProcessor<TIdentifier, string, TCommand, TResponse>
		where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier, TResponse>, ICommand<TIdentifier, string, TResponse>, IMessage<TIdentifier>, IMessage<TIdentifier, string> 
		where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier>, IMessage<TIdentifier, string>
    {
    }
}
