using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface IMessage<out TIdentifier> : IMessage<TIdentifier, byte[]>
		where TIdentifier : struct, IConvertible
    {
    }
}
