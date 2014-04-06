using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Interface IEventAction
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	internal interface IEventAction<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
		/// <summary>
		///     Invokes the specified evt.
		/// </summary>
		/// <param name="evt">The evt.</param>
		void Invoke(IEvent<TIdentifier, TStore> evt);

		/// <summary>
		///     Handles the event.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <param name="handled">if set to <c>true</c> [handled].</param>
		/// <returns>IEvent{`0`1}.</returns>
		IEvent<TIdentifier, TStore> HandleEvent(TStore store, ref bool handled);
	}
}