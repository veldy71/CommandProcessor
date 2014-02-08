using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
    public interface ICommand<out TIdentifier> : ICommand<TIdentifier, byte[]>
        where TIdentifier : struct, IConvertible
    {
        
    }
}