using System;

namespace Veldy.Net.CommandProcessor
{
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
