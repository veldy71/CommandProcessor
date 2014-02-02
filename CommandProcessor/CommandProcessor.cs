namespace Veldy.Net.CommandProcessor
{
    public class CommandProcessor<TStore, TCommand, TResponse> : ICommandProcessor<TStore, TCommand, TResponse>
        where TStore : class
        where TCommand : class, ICommand<TStore, TResponse>
        where TResponse : class, IResponse<TStore>, IMessage<TStore>, new()
    {
    }
}
