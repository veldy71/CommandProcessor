using System;

namespace Veldy.Net.CommandProcessor.Text
{
	public interface IResponse<out TIdentifier> : IResponse<TIdentifier, string>
		where TIdentifier : struct, IConvertible
	{
	}
}
