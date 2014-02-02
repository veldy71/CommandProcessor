using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class Message<TEnumMessageId> : IMessage
        where TEnumMessageId : struct, IConvertible
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message{TEnumMessageId}"/> class.
        /// </summary>
        protected Message()
        {
            Key = new Key<Message<TEnumMessageId>>(this);
        }

        /// <summary>
        /// Gets the delimeter.
        /// </summary>
        /// <value>
        /// The delimeter.
        /// </value>
        protected virtual char Delimeter { get { return ' '; } }

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public virtual TEnumMessageId MessageId
        {
            get { return (TEnumMessageId)Enum.ToObject(typeof(TEnumMessageId), Store[0]); }
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public IKey<IMessage<string>, string> Key { get; protected set; }

        /// <summary>
        /// Gets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public string Store { get; protected set; }

        /// <summary>
        /// Gets the store parts.
        /// </summary>
        /// <value>
        /// The store parts.
        /// </value>
        public string[] StoreParts { get { return Store.Split(Delimeter); } }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public virtual int CompareTo(string other)
        {
            return Key.CompareTo(other);
        }
    }
}
