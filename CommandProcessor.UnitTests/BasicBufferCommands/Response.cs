using System;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicCommands
{
    abstract class Response : Message, IResponse
    {
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
        }

        /// <summary>
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected override int BufferLength
        {
            get { return this.Store.Length; }
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
