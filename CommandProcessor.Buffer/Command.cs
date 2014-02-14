using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Class Command.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
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
