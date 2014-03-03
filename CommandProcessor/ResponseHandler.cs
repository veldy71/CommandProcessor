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
		/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
		/// <typeparam name="TResponse">The type of the t response.</typeparam>
		/// <param name="commandWithResponse">The command with response.</param>
		/// <param name="store">The store.</param>
		/// <returns>``1.</returns>
		public static TResponse HandleResponse<TCommandWithResponse, TResponse>(TCommandWithResponse commandWithResponse,
			TStore store)
			where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>,
				ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
			where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
		{
			var response = new TResponse();

			if (response.Key.IsMatch(store))
			{
				response.SetStore(store);

				return response;
			}

			return null;
		}
	}
}
