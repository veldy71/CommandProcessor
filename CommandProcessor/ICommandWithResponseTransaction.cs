using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Interface ICommandWithResponseTransaction
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public interface ICommandWithResponseTransaction<TIdentifier, TStore, out TCommand, out TResponse>
		: ICommandTransaction<TIdentifier, TStore, TCommand>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TCommand : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>,
			IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TStore : class
	{
		/// <summary>
		///     Gets the command with response.
		/// </summary>
		/// <value>
		///     The command with response.
		/// </value>
		TCommand CommandWithResponse { get; }

		/// <summary>
		///     Gets the response.
		/// </summary>
		/// <value>The response.</value>
		TResponse Response { get; }

		/// <summary>
		///     Gets a value indicating whether [waiting for response].
		/// </summary>
		/// <value><c>true</c> if [waiting for response]; otherwise, <c>false</c>.</value>
		bool WaitingForResponse { get; }

		/// <summary>
		///     Gets the awaiting response since timestamp.
		/// </summary>
		/// <value>The awaiting response since timestamp.</value>
		long AwaitingResponseSinceTimestamp { get; }

		/// <summary>
		///     Sets the response store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <returns></returns>
		bool SetResponseStore(TStore store);

		/// <summary>
		///     Sets the waiting for response.
		/// </summary>
		void SetWaitingForResponse();
	}
}