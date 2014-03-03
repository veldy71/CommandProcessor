﻿using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	/// <summary>
	///     Class CommandWithResponse.
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	internal abstract class CommandWithResponse<TResponse> : CommandWithResponse<MessageIdentifier, TResponse>,
		ICommandWithResponse<TResponse>
		where TResponse : class, IResponse, IResponse<MessageIdentifier>, new()
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="CommandWithResponse{TResponse}" /> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected CommandWithResponse(MessageIdentifier identifier)
			: base(identifier)
		{
		}
	}
}