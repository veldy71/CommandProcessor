using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public interface ICommand<out TIdentifier> : ICommand<TIdentifier, string>
        where TIdentifier : struct, IConvertible
    {

    }
}
