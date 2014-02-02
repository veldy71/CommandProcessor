namespace Veldy.Net.CommandProcessor
{
    public interface ICommand<TStore, out TResponse> : IMessage<TStore> 
        where TStore : class
        where TResponse : class, IResponse<TStore>, IMessage<TStore>
    {
        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        TResponse CreateResponse(TStore store);

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <returns></returns>
        TStore Execute();
    }
}
