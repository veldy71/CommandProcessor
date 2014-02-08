using System;

namespace Veldy.Net.CommandProcessor
{
	public interface ICommand<out TIdentifier, TStore> : IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible
		where TStore : class
	{
	}
}
