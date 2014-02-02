namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface ICommand<out TResponse> : ICommand<byte[], TResponse>
        where TResponse : class, IResponse, IResponse<byte[]>, IMessage, IMessage<byte[]>
    {
    }
}