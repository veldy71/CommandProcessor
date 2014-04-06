using System;

namespace CommandProcessor.Buffer.UnitTests
{
	abstract class Event : Message, IEvent
	{
		private byte[] _store;

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public override byte[] Store
		{
			get
			{
				if (_store == null)
				{
					_store = BitConverter.GetBytes(Convert.ToUInt16(Key.Identifier));
				}

				return _store;
			}
		}

		/// <summary>
		/// Sets the store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <exception cref="System.ArgumentNullException">store</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// store
		/// or
		/// store
		/// </exception>
		public void SetStore(byte[] store)
		{
			if (store == null)
				throw new ArgumentNullException("store");

			if (store.Length < 2)
				throw new ArgumentOutOfRangeException("store");

			if (_store == null || Key.IsMatch(store))
				_store = store;
			else
				throw new ArgumentOutOfRangeException("store");
		}
	}
}
