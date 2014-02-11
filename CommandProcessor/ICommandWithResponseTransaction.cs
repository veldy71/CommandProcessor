using System;

namespace Veldy.Net.CommandProcessor
{
    public interface ICommandWithResponseTransaction<TIdentifier, TStore, out TCommand, out TResponse> 
        : ICommandTransaction<TIdentifier, TStore, TCommand> 
        where TIdentifier : struct, IConvertible
        where TCommand : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
        where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
        where TStore : class
    {
        /// <summary>
        /// Gets the command with response.
        /// </summary>
        /// <value>
        /// The command with response.
        /// </value>
        TCommand CommandWithResponse { get; }

        /// <summary>
        /// Sets the response store.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        bool SetResponseStore(TStore store);
    }
}
