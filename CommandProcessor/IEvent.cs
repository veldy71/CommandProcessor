using System;

namespace Veldy.Net.CommandProcessor
{
    public interface IEvent<out TIdentifier, TStore> : IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible
        where TStore : class
    {
    }
}
