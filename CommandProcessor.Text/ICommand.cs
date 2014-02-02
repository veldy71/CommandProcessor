namespace Veldy.Net.CommandProcessor.Text
{
    public interface ICommand<out TResponse> : ICommand<string, TResponse>
        where TResponse : class, IResponse, IResponse<string>, IMessage, IMessage<string>
    {
    }
}
