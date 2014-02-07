using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface ICommand<out TIdentifer, out TResponse> : ICommand<TIdentifer, byte[], TResponse>
		where TIdentifer : struct, IConvertible
        where TResponse : class, IResponse<TIdentifer>, IResponse<TIdentifer, byte[]>, IMessage<TIdentifer>, IMessage<TIdentifer, byte[]>
    {
    }
}