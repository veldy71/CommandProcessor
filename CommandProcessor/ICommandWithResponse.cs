using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
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

        /// <summary>
        /// Gets the length of the response.
        /// </summary>
        /// <value>
        /// The length of the response.
        /// </value>
        int ResponseLength { get; }
    }
}
