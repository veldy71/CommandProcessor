using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Interface IAsynchronousCommandProcessor
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	public interface IAsynchronousCommandProcessor<in TIdentifier, in TCommand, in TCommandWithResponse, in TResponse, in TEvent>
		: IAsynchronousCommandProcessor<TIdentifier, byte[], TCommand, TCommandWithResponse, TResponse, TEvent>
		where TIdentifier : struct, IConvertible
		where TCommand : class, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, byte[], TResponse>, ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TResponse : class, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TEvent : class, IEvent<TIdentifier>, IEvent<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
	{
	}
}
