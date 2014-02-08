using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public interface ICommandProcessor<TIdentifier, TCommandWithResponse, TResponse> : ICommandProcessor<TIdentifier, string, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, string, TResponse>, ICommand<TIdentifier, string>
        where TResponse : class, IResponse<TIdentifier, string>, IMessage<TIdentifier, string>
    { }
}
