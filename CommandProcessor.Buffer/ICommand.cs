using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	///     Interface ICommand
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public interface ICommand<out TIdentifier> : ICommand<TIdentifier, byte[]>
		where TIdentifier : struct, IConvertible, IComparable<byte[]> 
	{
	}
}