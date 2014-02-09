using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class CommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
        : CommandProcessor<TIdentifier, string, TCommand, TCommandWithResponse, TResponse>,
            ICommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>,
            ICommandWithResponse<TIdentifier, string, TResponse>, ICommand<TIdentifier, string>,
            IMessage<TIdentifier, string>
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier, string>,
            new()
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
        protected abstract void PushCommandWithNoResponse(string store);

        /// <summary>
        /// Pushes the command with response.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="responseLength">Length of the response.</param>
        /// <returns></returns>
        protected abstract string PushCommandWithResponse(string store, int responseLength);
    }
}
