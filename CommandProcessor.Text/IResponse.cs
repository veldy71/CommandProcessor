using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	/// Interface IResponse
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public interface IResponse<out TIdentifier> : IResponse<TIdentifier, string>
		where TIdentifier : struct, IConvertible
	{
	}
}
