﻿namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	/// Class CommandWithResponse.
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	abstract class CommandWithResponse<TResponse> : Text.CommandWithResponse<MessageIdentifier, TResponse>, ICommandWithResponse<TResponse>
		where TResponse : class, IResponse, Text.IResponse<MessageIdentifier>, IResponse<MessageIdentifier, string>, new()
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CommandWithResponse{TResponse}"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected CommandWithResponse(MessageIdentifier identifier)
			: base(identifier)
		{
		}
	}
}
