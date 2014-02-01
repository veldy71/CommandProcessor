using System;

namespace Veldy.Net.CommandProcessor
{
    public interface IMessage<out TStore> where TStore : struct, IConvertible
    {
        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        TStore Store { get; }
    }
}
