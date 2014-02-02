using System;

namespace Veldy.Net.CommandProcessor
{
    public interface IKey<out TTarget, in TStore> : IComparable<TStore>
        where TTarget : class, IMessage<TStore>, IComparable<TStore>
        where TStore : class
    {
        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        TTarget Target { get; }
    }
}
