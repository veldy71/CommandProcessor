namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	abstract class CommandWithResponse<TResponse> : Buffer.CommandWithResponse<MessageIdentifier, TResponse>, ICommandWithResponse<TResponse> 
		where TResponse : class, IResponse, Buffer.IResponse<MessageIdentifier>, new()
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
