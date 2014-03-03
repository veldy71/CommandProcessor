using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Interface IResponse
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	public interface IResponse<out TIdentifier, TStore> : IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
		/// <summary>
		///     Sets the store.
		/// </summary>
		/// <param name="store">The store.</param>
		void SetStore(TStore store);
	}
}