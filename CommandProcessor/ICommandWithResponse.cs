using System;

namespace Veldy.Net.CommandProcessor
{
    public interface ICommandWithResponse<out TIdentifier, TStore, out TResponse> : ICommand<TIdentifier, TStore>
        where TIdentifier : struct, IConvertible
        where TStore : class
        where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
    {
        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        TResponse CreateResponse(TStore store);
    }
}
