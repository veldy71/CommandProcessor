using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class Message<TIdentifier> : IMessage<TIdentifier>
        where TIdentifier : struct, IConvertible
    {
	    private string _store = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message{TEnumMessageId}"/> class.
        /// </summary>
        protected Message(TIdentifier identifier)
        {
            Key = new Key<TIdentifier>(identifier);
	        _store = this.Key.Store;
        }

        /// <summary>
        /// Gets the delimeter.
        /// </summary>
        /// <value>
        /// The delimeter.
        /// </value>
        protected virtual char Delimeter { get { return ' '; } }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public IKey<TIdentifier, string> Key { get; protected set; }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>
		/// The store.
		/// </value>
		public virtual string Store
		{
			get
			{
				return _store;
			}
			protected set
			{
				_store = value ?? this.Key.Store;
			}
		}

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
