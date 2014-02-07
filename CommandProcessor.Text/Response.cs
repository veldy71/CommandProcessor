using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class Response<TIdentifier> : Message<TIdentifier>, IResponse<TIdentifier>
        where TIdentifier : struct, IConvertible
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Response{TIdentifier}"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected Response(TIdentifier identifier) : base(identifier)
		{ }

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
