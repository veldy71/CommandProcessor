using System;
using System.Diagnostics;
using System.Globalization;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class Command<TEnumMessageId, TResponse> : Message<TEnumMessageId>, ICommand<TResponse>
        where TResponse : class, IResponse, IResponse<string>, IMessage, IMessage<string>, new()
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
        public TResponse CreateResponse(string store)
        {
            if (Key.CompareTo(store) == 0)
            {
                var response = new TResponse();
                response.SetStore(store);

                return response;
            }

            return null;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <returns></returns>
        public virtual string Execute()
        {
            var type = typeof(TEnumMessageId);
            var memInfo = type.GetMember(MessageId.ToString(CultureInfo.InvariantCulture));
            var attributes = memInfo[0].GetCustomAttributes(typeof(EnumTextAttribute),
                false);
            Store = ((EnumTextAttribute)attributes[0]).Text;

            return Store;
        }
    }
}
