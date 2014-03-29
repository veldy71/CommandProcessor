using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Interface IAsynchronousCommandProcessor
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	/// <typeparam name="TEvent">The type of the t event.</typeparam>
	public interface IAsynchronousCommandProcessor<in TIdentifier, TStore, in TCommand, in TCommandWithResponse,
		in TResponse, in TEvent>
		: ICommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
			ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TEvent : class, IEvent<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		/// <summary>
		/// Gets the message wait.
		/// </summary>
		/// <value>The message wait.</value>
		int MessageWait { get; }

		/// <summary>
		/// Gets the event wait.
		/// </summary>
		/// <value>The event wait.</value>
		int EventWait { get; }

		/// <summary>
		///     Enqueues the message.
		/// </summary>
		/// <param name="store">The store.</param>
		void EnqueueMessage(TStore store);
	}
}