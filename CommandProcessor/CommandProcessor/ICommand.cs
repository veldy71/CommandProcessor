using System;

namespace Veldy.Net.CommandProcessor
{
    public interface ICommand<out TStore, TResponse> : IMessage<TStore>
        where TStore : struct, IConvertible
        where TResponse : class, IResponse, IMessage
    {
         TResponse CreateResponse(TStore store);
    }
}
