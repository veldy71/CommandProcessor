using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class ResponseHandler.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	public static class ResponseHandler<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
		/// <summary>
		/// Handles the response.
		/// </summary>
		/// <typeparam name="TResponse">The type of the t response.</typeparam>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <param name="store">The store.</param>
		/// <param name="postProcess">The post process.</param>
		/// <returns>``0.</returns>
		public static TResponse HandleResponse<TResponse>(ICommandWithResponse<TIdentifier, TStore, TResponse> commandWithResponse, TStore store, Action<ICommandWithResponse<TIdentifier, TStore, TResponse>, IResponse<TIdentifier, TStore>> postProcess = null)
			where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		{
			var response = new TResponse();

			if (response.Key.IsMatch(store))
			{
				response.SetStore(store);

				if (postProcess != null)
					postProcess(commandWithResponse, response);

				return response;
			}

			return null;
		}
	}
}
