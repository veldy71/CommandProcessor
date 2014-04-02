namespace CommandProcessor.Buffer.UnitTests
{
	class EchoEventArgs
	{
		/// <summary>
		/// Gets the payload.
		/// </summary>
		/// <value>The payload.</value>
		public byte[] Payload { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EchoEventArgs"/> class.
		/// </summary>
		/// <param name="payload">The payload.</param>
		public EchoEventArgs(byte[] payload)
		{
			Payload = payload;
		}
	}
}
