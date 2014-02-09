using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	public abstract class Command<TIdentifier> : Message<TIdentifier>, ICommand<TIdentifier>
		where TIdentifier : struct, IConvertible
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Command{TIdentifier}"/> class.
		/// </summary>
		/// <param name="messageId">The message identifier.</param>
		protected Command(TIdentifier messageId) 
			: base(messageId)
		{
		}
	}
}
