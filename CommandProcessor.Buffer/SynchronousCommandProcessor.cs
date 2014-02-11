using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	public abstract class SynchronousCommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
		: SynchronousCommandProcessor<TIdentifier, byte[], TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible
		where TCommand : class, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, byte[], TResponse>,
			ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TResponse : class, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
	{
	}
}
