using System;
using Veldy.Net.CommandProcessor.Text;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
    sealed class EchoComand : Command<MessageIdentifier, EchoResponse>
    {
        private byte[] _payload = new byte[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="EchoComand"/> class.
        /// </summary>
        public EchoComand() : base(MessageIdentifier.Echo)
        {
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

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        public override string Execute()
        {
            var store = base.Execute();

            return string.Format("{0}{1}{2}",
                store,
                Delimeter,
                Convert.ToBase64String(_payload));
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
            get { return StoreParts.Length > 1 ?  Convert.FromBase64String(StoreParts[1]) : new byte[0]; }
        }
    }
}
