using Veldy.Net.CommandProcess.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicCommands
{
    class EchoCommand : Command<MessageIdentifier, EchoResponse> 
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
            get { return base.BufferLength + this.PayLoad.Length; }
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        public override byte[] Execute()
        {
            var store = base.Execute();
            
            if (this.PayLoad != null && this.PayLoad.Length > 0)
                SetByteArray(store, 1, this.PayLoad);

            return store;
        }
    }

    class EchoResponse : Response<MessageIdentifier>
    {
        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public byte[] Payload
        {
            get { return GetByteArray(this.Store, 1, this.BufferLength - 1); }
        }
    }
}
