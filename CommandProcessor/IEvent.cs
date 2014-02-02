namespace Veldy.Net.CommandProcessor
{
    public interface IEvent<TStore> : IMessage<TStore>
        where TStore : class
    {
    }
}
