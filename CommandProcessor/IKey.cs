using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Interface IKey
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
    public interface IKey<out TIdentifier, TStore> : IComparable<TStore>
        where TIdentifier : struct, IConvertible
        where TStore : class
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
		/// The identifier.
        /// </value>
        TIdentifier Identifier { get; }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>
		/// The store.
		/// </value>
		TStore Store { get; }
    }
}
