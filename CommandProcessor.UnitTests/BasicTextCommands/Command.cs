﻿using Veldy.Net.CommandProcessor.Text;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	///     Class Command.
	/// </summary>
	internal abstract class Command : Command<MessageIdentifier>, ICommand
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Command" /> class.
		/// </summary>
		/// <param name="messageId">The message identifier.</param>
		protected Command(MessageIdentifier messageId)
			: base(messageId)
		{
		}
	}
}