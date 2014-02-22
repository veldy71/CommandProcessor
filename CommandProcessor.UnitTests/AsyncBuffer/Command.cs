namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	/// Class Command.
	/// </summary>
	abstract class Command : Buffer.Command<Identifier>, ICommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Command"/> class.
		/// </summary>
		/// <param name="messageId">The message identifier.</param>
		protected Command(Identifier messageId) 
			: base(messageId)
		{
		}
	}
}
