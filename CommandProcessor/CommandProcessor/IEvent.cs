using System;

namespace Veldy.Net.CommandProcessor
{
    public interface IEvent<out TStore> : IMessage<TStore>
        where TStore : struct, IConvertible
    {
    }
}
