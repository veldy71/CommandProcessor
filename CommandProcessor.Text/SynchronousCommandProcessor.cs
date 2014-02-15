using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	/// Class SynchronousCommandProcessor.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public abstract class SynchronousCommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
		: SynchronousCommandProcessor<TIdentifier, string, TCommand, TCommandWithResponse, TResponse>,
			ICommandProcessor<TIdentifier, TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible
		where TCommand : class, ICommand<TIdentifier>, ICommand<TIdentifier, string>, IMessage<TIdentifier, string>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TResponse>,
			ICommandWithResponse<TIdentifier, string, TResponse>, ICommand<TIdentifier, string>,
			IMessage<TIdentifier, string>
		where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier, string>, new()
	{
		/// <summary>
		/// Commands the processor.
		/// </summary>
		protected SynchronousCommandProcessor()
		{
		}
	}
}
