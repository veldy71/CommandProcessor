using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class CommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
		: SynchronousCommandProcessor<TIdentifier, string, TCommand, TCommandWithResponse, TResponse>,
            ICommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>,
            ICommandWithResponse<TIdentifier, string, TResponse>, ICommand<TIdentifier, string>,
            IMessage<TIdentifier, string>
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier, string>
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CommandProcessor{TIdentifier, TCommand, TCommandWithResponse, TResponse}"/> class.
		/// </summary>
		protected CommandProcessor()
        {
        }
    }
}
