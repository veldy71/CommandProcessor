namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface ICommandProcessor<TCommand, TResponse> : ICommandProcessor<byte[], TCommand, TResponse>
        where TCommand : class, ICommand<byte[], TResponse>
        where TResponse : class, IResponse<byte[]>, IMessage<byte[]>
    {
    }
}
