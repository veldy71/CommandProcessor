using System.Diagnostics;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class EchoCommand. This class cannot be inherited.
	/// </summary>
	sealed class EchoCommand : CommandWithResponse<EchoResponse>
	{
		private byte[] _payload = new byte[0];

		/// <summary>
		/// Initializes a new instance of the <see cref="EchoCommand"/> class.
		/// </summary>
		public EchoCommand()
		{
			Key = new Key(new Identifier(MessageIdentifier.Echo, MessageType.CommandType));
		}

		/// <summary>
		/// Gets the commandlength.
		/// </summary>
		/// <value>The commandlength.</value>
		public override int CommandLength
		{
			get { return base.CommandLength + _payload.Length; }
		}

		/// <summary>
		/// Gets the length of the response.
		/// </summary>
		/// <value>The length of the response.</value>
		public override int ResponseLength
		{
			get { return _payload.Length + 2; }
		}

		/// <summary>
		/// Setups the store.
		/// </summary>
		/// <param name="store">The store.</param>
		protected override void SetupStore(byte[] store)
		{
			SetByteArray(store, 2, Payload);
		}

		/// <summary>
		/// Gets or sets the payload.
		/// </summary>
		/// <value>The payload.</value>
		public byte[] Payload
		{
			get { return _payload; }
			set { _payload = value ?? new byte[0]; }
		}
	}

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
