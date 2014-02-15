using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicBufferCommands
{
	/// <summary>
	/// Class Response.
	/// </summary>
	class Response : Buffer.Response<MessageIdentifier>, IResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Response"/> class.
		/// </summary>
		public Response()
		{
			Key = new Key<MessageIdentifier>(MessageIdentifier.Echo);
		}
	}
}
