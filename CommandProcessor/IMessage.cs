using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Interface IMessage
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
    public interface IMessage<out TIdentifier, TStore> 
		where TIdentifier : struct, IConvertible
        where TStore : class
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        IKey<TIdentifier, TStore> Key { get; } 
        
        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        TStore Store { get; }
    }
}
