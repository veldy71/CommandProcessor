using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	/// Interface ICommandProcessor
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public interface ICommandProcessor<in TIdentifier, in TCommand, in TCommandWithResponse, in TResponse>
        : ICommandProcessor<TIdentifier, string, TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>, ICommandWithResponse<TIdentifier, string, TResponse>, ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
		where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier, string>
	{ }
}
