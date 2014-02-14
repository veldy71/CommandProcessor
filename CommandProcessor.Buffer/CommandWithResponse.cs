using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Class CommandWithResponse.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
    public abstract class CommandWithResponse<TIdentifier, TResponse> : Message<TIdentifier>, ICommandWithResponse<TIdentifier, TResponse>
        where TIdentifier : struct, IConvertible
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, byte[]>, IMessage<TIdentifier, byte[]>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandWithResponse{TIdentifier, TResponse}"/> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        protected CommandWithResponse(TIdentifier identifier)
            : base(identifier)
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
        /// Gets the length of the response.
        /// </summary>
        /// <value>
        /// The length of the response.
        /// </value>
        public abstract int ResponseLength { get; }

        /// <summary>
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected override int BufferLength { get { return this.Key.Store.Length; } }
    }

}
