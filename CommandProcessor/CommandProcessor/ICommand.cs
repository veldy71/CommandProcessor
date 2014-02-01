using System;

namespace Veldy.Net.CommandProcessor
{
    public interface ICommand<out TStore, TResponse> : IMessage<TStore>
        where TStore : struct, IConvertible
        where TResponse : class, IResponse<TStore>, IMessage<TStore>
    {
         TResponse CreateResponse(TStore store);
    }
}
