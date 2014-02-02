using System;
using Veldy.Net.CommandProcessor;

namespace Veldy.Net.CommandProcess.Buffer
{
    public abstract class Command<TEnumMessageId, TResponse> : Message<TEnumMessageId>, ICommand<TResponse>
        where TResponse : class, IResponse, IResponse<byte[]>, IMessage, IMessage<byte[]>, new()
        where TEnumMessageId : struct, IConvertible
    {
        private readonly TEnumMessageId _messageId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command{TEnumMessageId, TResponse}"/> class.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        protected Command(TEnumMessageId messageId)
        {
            _messageId = messageId;
        }

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public override TEnumMessageId MessageId
        {
            get { return _messageId; }
        }

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        public TResponse CreateResponse(byte[] store)
        {
            if (this.Key.CompareTo(store) == 0)
            {
                var response = new TResponse();
                response.SetStore(store);

                return response;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <returns></returns>
        public virtual byte[] Execute()
        {
            this.Store = new byte[this.BufferLength];
            this.Store[0] = (byte)Convert.ToByte(_messageId);

            return this.Store;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public override int CompareTo(byte[] other)
        {
            // compare the first byte only
            return Store[0].CompareTo(other[0]);
        }

        /// <summary>
        /// Gets the length of the buffer.
        /// </summary>
        /// <value>
        /// The length of the buffer.
        /// </value>
        protected override int BufferLength { get { return 1; } }
    }
}
