namespace Veldy.Net.CommandProcessor.Text
{
    public interface ICommandProcessor<TCommand, TResponse> : ICommandProcessor<string, TCommand, TResponse>
        where TCommand : class, ICommand<string, TResponse>
        where TResponse : class, IResponse<string>, IMessage<string>
    {
    }
}
