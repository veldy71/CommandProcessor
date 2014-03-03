using Veldy.Net.CommandProcessor.Buffer;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Class EchoCommand. This class cannot be inherited.
	/// </summary>
	internal sealed class EchoCommand : CommandWithResponse<EchoResponse>
	{
		private byte[] _payload = new byte[0];

		/// <summary>
		///     Initializes a new instance of the <see cref="EchoCommand" /> class.
		/// </summary>
		public EchoCommand()
			: base(new Identifier {MessageIdentifier = MessageIdentifier.Echo, MessageType = MessageType.CommandType})
		{
		}

		/// <summary>
		///     Gets or sets the pay load.
		/// </summary>
		/// <value>
		///     The pay load.
		/// </value>
		public byte[] PayLoad
		{
			get { return _payload; }
			set { _payload = value ?? new byte[0]; }
		}

		/// <summary>
		///     Gets the length of the response.
		/// </summary>
		/// <value>
		///     The length of the response.
		/// </value>
		public override int ResponseLength
		{
			get { return PayLoad.Length; }
		}

		/// <summary>
		///     Gets the length of the buffer.
		/// </summary>
		/// <value>
		///     The length of the buffer.
		/// </value>
		protected override int BufferLength
		{
			get { return base.BufferLength + PayLoad.Length; }
		}

		/// <summary>
		///     Populates the store.
		/// </summary>
		/// <param name="store">The store.</param>
		protected override void PopulateStore(byte[] store)
		{
			SetByteArray(store, Key.Store.Length, PayLoad);
		}
	}

	internal sealed class EchoResponse : Response, IMessage<Identifier, byte[]>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="EchoResponse" /> class.
		/// </summary>
		public EchoResponse()
		{
			Key =
				new Key<Identifier>(new Identifier
				{
					MessageIdentifier = MessageIdentifier.Echo,
					MessageType = MessageType.ResponseType
				});
		}

		/// <summary>
		///     Gets the payload.
		/// </summary>
		/// <value>
		///     The payload.
		/// </value>
		public byte[] Payload
		{
			get { return GetByteArray(Store, Key.Store.Length, BufferLength - 1); }
		}
	}

	/// <summary>
	///     Class EchoEvent.
	/// </summary>
	internal class EchoEvent : Event
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="EchoEvent" /> class.
		/// </summary>
		public EchoEvent()
		{
			Key =
				new Key<Identifier>(new Identifier {MessageIdentifier = MessageIdentifier.Echo, MessageType = MessageType.EventType});
		}
	}
}