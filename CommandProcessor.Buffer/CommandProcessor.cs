using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	///     Class CommandProcessor.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public abstract class CommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
		: CommandProcessor<TIdentifier, byte[], TCommand, TCommandWithResponse, TResponse>,
			ICommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible, IComparable<byte[]> 
		where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>,
			ICommandWithResponse<TIdentifier, byte[], TResponse>, ICommand<TIdentifier, byte[]>,
			IMessage<TIdentifier, byte[]>
		where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>, new()
	{
	}
}