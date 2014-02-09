﻿using System;
using System.Text;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
    sealed class ByteBufferEchoCommand : CommandWithResponse<EchoResponse>
    {
        private byte[] _payload = new byte[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteBufferEchoCommand"/> class.
        /// </summary>
        public ByteBufferEchoCommand() : base(MessageIdentifier.Echo)
        {
        }

		/// <summary>
		/// Populates the store.
		/// </summary>
		/// <param name="store">The store.</param>
	    protected override void PopulateStore(ref string store)
	    {
		    var sb = new StringBuilder();
			sb.Append(store).Append(this.Delimeter).Append(Convert.ToBase64String(this.Payload));

		    store = sb.ToString();
	    }

        /// <summary>
        /// Gets the length of the response.
        /// </summary>
        /// <value>
        /// The length of the response.
        /// </value>
        public override int ResponseLength
        {
            get { return 2; }
        }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public byte[] Payload
        {
            get { return _payload; }
            set { _payload = value ?? new byte[0]; }
        }
    }

    sealed class EchoResponse : Response
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
            get { return StoreParts.Length > 1 ?  Convert.FromBase64String(StoreParts[1]) : new byte[0]; }
        }
    }
}
