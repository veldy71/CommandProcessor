using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Class Response.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
    public abstract class Response<TIdentifier> : Message<TIdentifier>, IResponse<TIdentifier>
        where TIdentifier : struct, IConvertible
    {
	    private int _storeLength = 0;

	    /// <summary>
	    /// Initializes a new instance of the <see cref="Response{TIdentifier}"/> class.
	    /// </summary>
	    /// <param name="identifier">The identifier.</param>
	    protected Response(TIdentifier identifier) : base(identifier)
	    {
		    _storeLength = this.Key.Store.Length;
	    }

        /// <summary>
        /// Sets the store.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <exception cref="System.ArgumentNullException">store</exception>
        public void SetStore(byte[] store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            Store = store;
	        _storeLength = store.Length;
        }

        /// <summary>
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected override int BufferLength
        {
            get { return _storeLength; }
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public override int CompareTo(byte[] other)
        {
            // only compare the first byte
            return Store[0].CompareTo(other[0]);
        }
    }
}
