using System;

namespace Veldy.Net.CommandProcessor
{
    public interface ICommandWithResponse<out TIdentifier, TStore, out TResponse> : ICommand<TIdentifier, TStore>
        where TIdentifier : struct, IConvertible
        where TStore : class
        where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
    {
        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        TResponse CreateResponse(TStore store);

        /// <summary>
        /// Gets the length of the response.
        /// </summary>
        /// <value>
        /// The length of the response.
        /// </value>
        int ResponseLength { get; }
    }
}
