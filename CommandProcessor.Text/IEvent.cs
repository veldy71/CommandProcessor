using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	/// Interface IEvent
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public interface IEvent<out TIdentifier> : IEvent<TIdentifier, string>
		where TIdentifier : struct, IConvertible
	{
	}
}
