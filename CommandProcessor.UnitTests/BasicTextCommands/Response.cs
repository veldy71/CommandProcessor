namespace Veldy.Net.CommandProcessor.UnitTests.BasicTextCommands
{
	/// <summary>
	/// Class Response.
	/// </summary>
	abstract class Response : Text.Response<MessageIdentifier>, IResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Response"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected Response(MessageIdentifier identifier)
			: base(identifier)
		{
		}
	}
}
