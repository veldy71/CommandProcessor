namespace CommandProcessor.Buffer.UnitTests
{
	enum MessageIdentifier : byte
	{
		/// <summary>
		/// The echo message.
		/// </summary>
		Echo = 0x01
	}

	enum MessageType : byte
	{
		/// <summary>
		/// The command type
		/// </summary>
		CommandType = 0x01,

		/// <summary>
		/// The response type
		/// </summary>
		ResponseType = 0x02,

		/// <summary>
		/// The event type
		/// </summary>
		EventType = 0x03
	}
}
