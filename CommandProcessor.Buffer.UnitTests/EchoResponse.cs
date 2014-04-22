using System.Diagnostics;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class EchoResponse. This class cannot be inherited.
	/// </summary>
	sealed class EchoResponse : Response
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EchoResponse"/> class.
		/// </summary>
		public EchoResponse()
		{
			Key = new Key(new Identifier(MessageIdentifier.Echo, MessageType.ResponseType));
		}

		/// <summary>
		/// Gets the payload.
		/// </summary>
		/// <value>The payload.</value>
		public byte[] Payload
		{
			get { return GetByteArray(Store, 2, ResponseLength - 2); }
		}

		/// <summary>
		/// Sets the payload.
		/// </summary>
		/// <param name="payload">The payload.</param>
		/// <remarks>This is for internal use only in this example.</remarks>
		internal void SetPayload(byte[] payload)
		{
			Debug.Assert(payload.Length == ResponseLength - 2, "Something went wrong!");

			SetByteArray(Store, 2, payload);
		}
	}
}