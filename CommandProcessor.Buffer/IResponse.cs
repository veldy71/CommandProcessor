using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface IResponse<out TIdentifier> : IResponse<TIdentifier, byte[]>
		where TIdentifier : struct, IConvertible
    {
    }
}
