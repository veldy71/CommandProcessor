using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public abstract class CommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
        : CommandProcessor<TIdentifier, byte[], TCommand, TCommandWithResponse, TResponse>,
            ICommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>,
            ICommandWithResponse<TIdentifier, byte[], TResponse>, ICommand<TIdentifier, byte[]>,
            IMessage<TIdentifier, byte[]>
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
    {
        /// <summary>
        /// Commands the processor.
        /// </summary>
        protected CommandProcessor()
        {
            SetupPushCommandFunctions(PushCommandWithNoResponse, PushCommandWithResponse);
        }

        /// <summary>
        /// Pushes the command with no response.
        /// </summary>
        /// <param name="store">The store.</param>
        protected abstract void PushCommandWithNoResponse(byte[] store);

        /// <summary>
        /// Pushes the command with response.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="responseLength">Length of the response.</param>
        /// <returns></returns>
        protected abstract byte[] PushCommandWithResponse(byte[] store, int responseLength);
    }
}
