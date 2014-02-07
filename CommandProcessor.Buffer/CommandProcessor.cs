using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	public abstract class CommandProcessor<TIdenifier, TCommand, TResponse> : CommandProcessor<TIdenifier, byte[], TCommand, TResponse>, ICommandProcessor<TIdenifier, TCommand, TResponse>
		where TIdenifier : struct, IConvertible
		where TCommand : class, ICommand<TIdenifier, TResponse>, ICommand<TIdenifier, byte[], TResponse>, IMessage<TIdenifier>, IMessage<TIdenifier, byte[]> 
		where TResponse : class, IResponse<TIdenifier>, IResponse<TIdenifier, byte[]>, IMessage<TIdenifier>, IMessage<TIdenifier, byte[]>, new()
    {
    }
}
