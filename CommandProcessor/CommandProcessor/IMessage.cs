using System;

namespace Veldy.Net.CommandProcessor
{
    public interface IMessage<TStore> : IComparable<TStore>
        where TStore : class
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        IKey<IMessage<TStore>, TStore> Key { get; } 
        
        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        TStore Store { get; }
    }
}
