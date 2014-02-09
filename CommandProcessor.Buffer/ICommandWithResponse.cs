using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface ICommandWithResponse<out TIdentifer, out TResponse> : ICommandWithResponse<TIdentifer, byte[], TResponse>
        where TIdentifer : struct, IConvertible
        where TResponse : class, IResponse<TIdentifer>, IResponse<TIdentifer, byte[]>, IMessage<TIdentifer, byte[]>
    {
    }
}
