using System;

namespace Veldy.Net.CommandProcessor
{
    public abstract class CommandProcessor<TIdentifier, TStore, TCommand, TCommandResponse, TResponse> 
        : ICommandProcessor<TIdentifier, TStore, TCommand, TCommandResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TStore : class
        where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore> 
        where TCommandResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore> 
        where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
    {
        /// <summary>
        /// Sends the command with a response.
        /// </summary>
        /// <typeparam name="TRsp">The type of the RSP.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public TRsp SendCommand<TRsp>(TCommandResponse command) where TRsp : class, TResponse, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
        {
            // TODO
            return null;
        }

        /// <summary>
        /// Sends the command without a response.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommand(TCommand command)
        {
            // TODO
        }
    }
}
