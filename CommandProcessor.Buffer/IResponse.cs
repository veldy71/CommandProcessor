using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Interface IResponse
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
    public interface IResponse<out TIdentifier> : IResponse<TIdentifier, byte[]>
		where TIdentifier : struct, IConvertible
    {
    }
}
