using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Interface IEvent
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public interface IEvent<out TIdentifier> : IEvent<TIdentifier, byte[]>
		where TIdentifier : struct, IConvertible
	{
	}
}
