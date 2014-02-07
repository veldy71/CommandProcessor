using System;

namespace Veldy.Net.CommandProcessor
{
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
