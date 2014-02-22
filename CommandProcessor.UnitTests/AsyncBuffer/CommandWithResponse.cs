using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	/// Class CommandWithResponse.
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	internal abstract class CommandWithResponse<TResponse>
		: Buffer.CommandWithResponse<Identifier, TResponse>, ICommandWithResponse<TResponse> where TResponse : class, IResponse, IResponse<Identifier>, IResponse<Identifier, byte[]>,
			IMessage<Identifier, byte[]>, new()
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CommandWithResponse{TResponse}"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected CommandWithResponse(Identifier identifier) 
			: base(identifier)
		{
		}
	}
}
	