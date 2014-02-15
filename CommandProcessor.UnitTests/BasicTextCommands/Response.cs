using Veldy.Net.CommandProcessor.Text;

namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	/// Class Response.
	/// </summary>
	class Response : Text.Response<MessageIdentifier>, IResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Response" /> class.
		/// </summary>
		public Response()
		{
			Key = new Key<MessageIdentifier>(MessageIdentifier.Echo);
		}
	}
}
