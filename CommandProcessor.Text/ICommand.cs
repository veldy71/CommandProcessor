using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	/// Interface ICommand
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
    public interface ICommand<out TIdentifier> : ICommand<TIdentifier, string>
        where TIdentifier : struct, IConvertible
    {
    }
}
