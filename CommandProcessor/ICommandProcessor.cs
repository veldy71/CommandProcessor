namespace Veldy.Net.CommandProcessor
{
    public interface ICommandProcessor<TStore, TCommand, TResponse>
        where TStore : class
        where TCommand : class, ICommand<TStore, TResponse>
        where TResponse : class, IResponse<TStore>, IMessage<TStore>
    {
    }
}
