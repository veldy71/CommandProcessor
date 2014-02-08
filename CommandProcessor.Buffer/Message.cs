using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public abstract class Message<TIdentifier> : IMessage<TIdentifier, byte[]>
        where TIdentifier : struct, IConvertible
    {
	    private byte[] _store = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message{TEnumMessageId}"/> class.
        /// </summary>
        protected Message(TIdentifier messageId)
        {
			Key = new Key<TIdentifier>(messageId);
        }

        /// <summary>
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected virtual int BufferLength { get { return this.Key.Store.Length; } }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public IKey<TIdentifier, byte[]> Key { get; protected set; }

	    /// <summary>
	    /// Gets the store.
	    /// </summary>
	    /// <value>
	    /// The store.
	    /// </value>
	    public virtual byte[] Store
	    {
		    get
		    {
			    if (_store == null || _store.Length != this.BufferLength)
			    {
				    _store = new byte[this.BufferLength];
			    }

			    return _store;
		    }
		    protected set { _store = value ?? new byte[this.Key.Store.Length]; }
	    }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public virtual int CompareTo(byte[] other)
        {
            return Key.CompareTo(other);
        }

        /// <summary>
        /// Sets the byte array.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">
        /// store
        /// or
        /// value
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        protected static void SetByteArray(byte[] store, int index, byte[] value)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            if (value == null)
                throw new ArgumentNullException("value");

            if (index < 0)
                throw new ArgumentOutOfRangeException("index");

            if (index >= store.Length)
                throw new ArgumentOutOfRangeException("index");

            if (store.Length < index + value.Length)
                throw new ArgumentOutOfRangeException("value");

            System.Buffer.BlockCopy(value, 0, store, index, value.Length);
        }

        /// <summary>
        /// Gets the byte array.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="index">The index.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">store</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// index
        /// or
        /// index
        /// or
        /// length
        /// </exception>
        protected static byte[] GetByteArray(byte[] store, int index, int length)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            if (index < 0)
                throw new ArgumentOutOfRangeException("index");

            if (index >= store.Length)
                throw new ArgumentOutOfRangeException("index");

            if (length < 1 || store.Length < index + length)
                throw new ArgumentOutOfRangeException("length");

            var buffer = new byte[length];
            System.Buffer.BlockCopy(store, index, buffer, 0, length);

            return buffer;
        }
    }
}
