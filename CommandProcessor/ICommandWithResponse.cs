using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	public interface ICommandWithResponse<out TIdentifier, TStore> : ICommand<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
	}

	/// <summary>
	///     Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public interface ICommandWithResponse<out TIdentifier, TStore, out TResponse> : ICommandWithResponse<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
		/// <summary>
		/// Creates the response.
		/// </summary>
		/// <returns>`2.</returns>
		TResponse CreateResponse();
	}
}