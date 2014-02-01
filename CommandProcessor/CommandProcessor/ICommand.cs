using System;

namespace Veldy.Net.CommandProcessor
{
    public interface ICommand<out TStore> : IMessage<TStore>
        where TStore : struct, IConvertible
    {
    }
}
