namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class CommandProcessor<TCommand, TResponse> : CommandProcessor<string, TCommand, TResponse>, ICommandProcessor<TCommand, TResponse>
        where TCommand : class, ICommand<string, TResponse>
        where TResponse : class, IResponse<string>, IMessage<string>, new()
    {
    }
}
