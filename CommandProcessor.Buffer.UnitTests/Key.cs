using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class Key.
	/// </summary>
	class Key : Key<Identifier, byte[]>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Key"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		public Key(Identifier identifier) : base(identifier)
		{
		}

		/// <summary>
		/// Compares to.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns>System.Int32.</returns>
		public override int CompareTo(byte[] other)
		{
			var otherId = BitConverter.ToUInt16(other, 0);
			var id = Convert.ToUInt16(Identifier);

			return id.CompareTo(otherId);
		}

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public override byte[] Store
		{
			get { return BitConverter.GetBytes(Convert.ToUInt16(Identifier)); }
		}
	}
}
