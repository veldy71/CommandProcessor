using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
    sealed class EchoCommand : Command<MessageIdentifier, EchoResponse> 
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
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected override int BufferLength
        {
            get { return base.BufferLength + PayLoad.Length; }
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        public override byte[] Execute()
        {
            var store = base.Execute();
            
            if (PayLoad != null && PayLoad.Length > 0)
                SetByteArray(store, 1, PayLoad);

            return store;
        }
    }

    sealed class EchoResponse : Response<MessageIdentifier>
    {
        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public byte[] Payload
        {
            get { return GetByteArray(Store, 1, BufferLength - 1); }
        }
    }
}
