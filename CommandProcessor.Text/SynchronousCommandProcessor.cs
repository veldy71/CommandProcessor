using System;

namespace Veldy.Net.CommandProcessor.Text
{
	public abstract class SynchronousCommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
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
		/// Commands the processor.
		/// </summary>
		protected SynchronousCommandProcessor()
		{
		}
	}
}
