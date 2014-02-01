using System;

namespace Veldy.Net.CommandProcessor
{
    public interface IResponse<out TStore> : IMessage<TStore>
        where TStore : struct, IConvertible
    {
    }
}
