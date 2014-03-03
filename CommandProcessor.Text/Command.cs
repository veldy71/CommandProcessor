using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	///     Class Command.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public abstract class Command<TIdentifier> : Message<TIdentifier>, ICommand<TIdentifier>
		where TIdentifier : struct, IConvertible, IComparable<string> 
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Command{TIdentifier}" /> class.
		/// </summary>
		/// <param name="messageId">The message identifier.</param>
		protected Command(TIdentifier messageId)
			: base(messageId)
		{
		}
	}
}