using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Interface ICommandWithResponse
	/// </summary>
	/// <typeparam name="TIdentifer">The type of the t identifer.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
    public interface ICommandWithResponse<out TIdentifer, out TResponse> : ICommandWithResponse<TIdentifer, byte[], TResponse>
        where TIdentifer : struct, IConvertible
        where TResponse : class, IResponse<TIdentifer>, IResponse<TIdentifer, byte[]>, IMessage<TIdentifer, byte[]>
    {
    }
}
