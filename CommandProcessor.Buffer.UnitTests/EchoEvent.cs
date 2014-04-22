namespace CommandProcessor.Buffer.UnitTests
{
	sealed class EchoEvent : Event
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EchoEvent"/> class.
		/// </summary>
		public EchoEvent()
		{
			Key = new Key(new Identifier(MessageIdentifier.Echo, MessageType.EventType));
		}

		/// <summary>
		/// Gets the payload.
		/// </summary>
		/// <value>The payload.</value>
		public byte[] Payload
		{
			get { return GetByteArray(Store, 2, Store.Length - 2); }
		}
	}
}