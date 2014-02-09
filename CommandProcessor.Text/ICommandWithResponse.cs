using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public interface ICommandWithResponse<out TIdentifer, out TResponse> : ICommandWithResponse<TIdentifer, string, TResponse>
        where TIdentifer : struct, IConvertible
        where TResponse : class, IResponse<TIdentifer>, IResponse<TIdentifer, string>, IMessage<TIdentifer, string>
    {
    }
}
