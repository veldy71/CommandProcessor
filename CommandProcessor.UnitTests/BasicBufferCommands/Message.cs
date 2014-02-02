using System;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicCommands
{
    abstract class Message : IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        protected Message()
        {
            this.Key = new Key<Message>(this);
        }

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public virtual MessageId MessageId { get { return (BasicCommands.MessageId)this.Store[0]; } }

        /// <summary>
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected virtual int BufferLength { get { return 1; } }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public IKey<IMessage<byte[]>, byte[]> Key { get; protected set; }

        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public byte[] Store { get; protected set; }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public abstract int CompareTo(byte[] other);

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

            Buffer.BlockCopy(value, 0, store, index, value.Length);
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
            Buffer.BlockCopy(store, index, buffer, 0, length);

            return buffer;
        }
    }
}
