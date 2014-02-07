using System;

namespace Veldy.Net.CommandProcessor.Text
{
	public interface IMessage<out TIdentifier> : IMessage<TIdentifier, string>
		where TIdentifier : struct, IConvertible
	{
		/// <summary>
		/// Gets the store parts.
		/// </summary>
		/// <value>
		/// The store parts.
		/// </value>
		string[] StoreParts { get; }
	}
}
