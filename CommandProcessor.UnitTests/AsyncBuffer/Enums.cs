namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Enum MessageIdentifier
	/// </summary>
	internal enum MessageIdentifier : byte
	{
		/// <summary>
		///     Echo
		/// </summary>
		Echo = 0x01
	}

	/// <summary>
	///     Enum MessageType
	/// </summary>
	internal enum MessageType : byte
	{
		/// <summary>
		///     The command type
		/// </summary>
		CommandType = 0x01,

		/// <summary>
		///     The response type
		/// </summary>
		ResponseType = 0x02,

		/// <summary>
		///     The event type
		/// </summary>
		EventType = 0x03
	}
}