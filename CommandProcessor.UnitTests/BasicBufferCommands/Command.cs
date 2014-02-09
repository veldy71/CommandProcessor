namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	abstract class Command : Buffer.Command<MessageIdentifier>, ICommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Command"/> class.
		/// </summary>
		/// <param name="messageId">The message identifier.</param>
		protected Command(MessageIdentifier messageId) 
			: base(messageId)
		{
		}
	}
}
