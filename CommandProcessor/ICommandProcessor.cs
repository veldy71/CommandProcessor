using System;

namespace Veldy.Net.CommandProcessor
{
	public interface ICommandProcessor<TIdentifier, TStore, TCommand, TResponse>
		where TIdentifier : struct, IConvertible
        where TStore : class
		where TCommand : class, ICommand<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore> 
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
    {
    }
}
