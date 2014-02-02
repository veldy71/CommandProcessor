using Veldy.Net.CommandProcessor;

namespace Veldy.Net.CommandProcess.Buffer
{
    public interface ICommand<out TResponse> : ICommand<byte[], TResponse>
        where TResponse : class, IResponse, IResponse<byte[]>, IMessage, IMessage<byte[]>
    {
    }
}