namespace Veldy.Net.CommandProcessor
{
    public interface IResponse<TStore> : IMessage<TStore>
       where TStore : class
    {
        /// <summary>
        /// Sets the store.
        /// </summary>
        /// <param name="store">The store.</param>
        void SetStore(TStore store);
    }
}
