namespace Veldy.Net.CommandProcessor.Buffer
{
    public abstract class CommandProcessor<TCommand, TResponse> : CommandProcessor<byte[], TCommand, TResponse>, ICommandProcessor<TCommand, TResponse>
        where TCommand : class, ICommand<byte[], TResponse>
        where TResponse : class, IResponse<byte[]>, IMessage<byte[]>, new()
    {
    }
}
