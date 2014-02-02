using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class Response<TEnumMessageId> : Message<TEnumMessageId>, IResponse
        where TEnumMessageId : struct, IConvertible
    {
        /// <summary>
        /// Sets the store.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <exception cref="System.ArgumentNullException">store</exception>
        public void SetStore(string store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            Store = store;
        }
    }
}
