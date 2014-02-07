using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	public abstract class Command<TIdentifier, TResponse> : Message<TIdentifier>, ICommand<TIdentifier, TResponse>
		where TIdentifier : struct, IConvertible
		where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier>, IMessage<TIdentifier, byte[]>, new()
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Command{TIdentifier, TResponse}"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
        protected Command(TIdentifier identifier) : base(identifier)
        { }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>
		/// The store.
		/// </value>
	    public override byte[] Store
	    {
		    get
		    {
			    var store = base.Store;

			    System.Buffer.BlockCopy(this.Key.Store, 0, store, 0, this.Key.Store.Length);

				PopulateStore(store);

			    return store;
		    }
	    }

		/// <summary>
		/// Populates the store.
		/// </summary>
		/// <param name="store">The store.</param>
	    protected abstract void PopulateStore(byte[] store);

	    /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        public TResponse CreateResponse(byte[] store)
        {
			var response = new TResponse();

            if (response.Key.CompareTo(store) == 0)
            {
                response.SetStore(store);

                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected override int BufferLength { get { return this.Key.Store.Length; } }
    }
}
