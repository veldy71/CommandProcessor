using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	///     Interface IAsynchronousCommandProcessor
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	public interface IAsynchronousCommandProcessor<in TIdentifier, in TCommand, in TCommandWithResponse, in TResponse,
		in TEvent>
		: IAsynchronousCommandProcessor<TIdentifier, string, TCommand, TCommandWithResponse, TResponse, TEvent>
		where TIdentifier : struct, IConvertible, IComparable<string> 
		where TCommand : class, ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, string, TResponse>,
			ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
		where TResponse : class, IResponse<TIdentifier, string>, IMessage<TIdentifier, string>
		where TEvent : class, IEvent<TIdentifier>, IEvent<TIdentifier, string>, IMessage<TIdentifier, string>
	{
	}
}