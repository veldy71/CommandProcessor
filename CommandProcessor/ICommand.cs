using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Interface ICommand
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	public interface ICommand<out TIdentifier, TStore> : IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
	}
}