using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Class AsynchronousCommandProcessor.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	public abstract class AsynchronousCommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse, TEvent>
		: AsynchronousCommandProcessor<TIdentifier, byte[], TCommand, TCommandWithResponse, TResponse, TEvent>,
		IAsynchronousCommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse, TEvent>
		where TIdentifier : struct, IConvertible
		where TCommand : class, ICommandWithResponse<TIdentifier, byte[], TResponse> 
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, byte[], TResponse>,
			ICommand<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>
		where TResponse : class, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>, new()
		where TEvent : class, IEvent<TIdentifier>, IEvent<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>, new()
	{ }
}
