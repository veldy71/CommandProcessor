using System;

namespace Veldy.Net.CommandProcessor
{
    public interface IResponse<out TIdentifier, TStore> : IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible
		where TStore : class
    {
        /// <summary>
        /// Sets the store.
        /// </summary>
        /// <param name="store">The store.</param>
        void SetStore(TStore store);
    }
}
