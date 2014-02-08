using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
    sealed class EchoCommand : CommandWithResponse<MessageIdentifier, EchoResponse> 
    {
        private byte[] _payload = new byte[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="EchoCommand"/> class.
        /// </summary>
        public EchoCommand()
            : base(MessageIdentifier.Echo)
        { }

        /// <summary>
        /// Gets or sets the pay load.
        /// </summary>
        /// <value>
        /// The pay load.
        /// </value>
        public byte[] PayLoad
        {
            get { return _payload; }
            set { _payload = value ?? new byte[0]; }
        }

		/// <summary>
		/// Populates the store.
		/// </summary>
		/// <param name="store">The store.</param>
	    protected override void PopulateStore(byte[] store)
	    {
		    SetByteArray(store, this.Key.Store.Length, this.PayLoad);
	    }

	    /// <summary>
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected override int BufferLength
        {
            get { return base.BufferLength + PayLoad.Length; }
        }
    }

    sealed class EchoResponse : Response<MessageIdentifier>
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="EchoResponse"/> class.
		/// </summary>
		public EchoResponse()
			: base(MessageIdentifier.Echo)
	    {
	    }

	    /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public byte[] Payload
        {
            get { return GetByteArray(Store, this.Key.Store.Length, BufferLength - 1); }
        }
    }
}
