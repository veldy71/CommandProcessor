using System;

namespace Veldy.Net.CommandProcessor
{
	public interface ICommandWithResponseTransaction<TIdentifier, TStore, out TCommandWithResponse> : ICommandTransaction<TIdentifier, TStore, TCommandWithResponse>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
		/// <summary>
		///     Gets a value indicating whether [waiting for response].
		/// </summary>
		/// <value><c>true</c> if [waiting for response]; otherwise, <c>false</c>.</value>
		bool WaitingForResponse { get; }

		/// <summary>
		/// Gets a value indicating whether [response expired].
		/// </summary>
		/// <value><c>true</c> if [response expired]; otherwise, <c>false</c>.</value>
		bool ResponseExpired { get; }

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

		/// <summary>
		/// Gets the command with response.
		/// </summary>
		/// <value>The command with response.</value>
		ICommandWithResponse<TIdentifier, TStore> CommandWithResponse { get; } 
	}

	/// <summary>
	///     Interface ICommandWithResponseTransaction
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public interface ICommandWithResponseTransaction<TIdentifier, TStore, out TCommandWithResponse, out TResponse>
		: ICommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommandWithResponse<TIdentifier, TStore>, ICommand<TIdentifier, TStore>,
			IMessage<TIdentifier, TStore>
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TStore : class
	{
		/// <summary>
		///     Gets the response.
		/// </summary>
		/// <value>The response.</value>
		TResponse Response { get; }
	}
}