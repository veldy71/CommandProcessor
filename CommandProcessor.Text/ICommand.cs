using System;

namespace Veldy.Net.CommandProcessor.Text
{
	public interface ICommand<out TIdentifer, out TResponse> : ICommand<TIdentifer, string, TResponse>
		where TIdentifer : struct, IConvertible
		where TResponse : class, IResponse<TIdentifer>, IResponse<TIdentifer, string>, IMessage<TIdentifer>, IMessage<TIdentifer, string>
	{
	}
}
