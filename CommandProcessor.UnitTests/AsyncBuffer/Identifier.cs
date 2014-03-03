using System;

namespace Veldy.Net.CommandProcessor.UnitTests.AsyncBuffer
{
	/// <summary>
	///     Struct Identifier
	/// </summary>
	internal struct Identifier : IConvertible
	{
		/// <summary>
		///     Gets or sets the message identifier.
		/// </summary>
		/// <value>The message identifier.</value>
		public MessageIdentifier MessageIdentifier { get; set; }

		/// <summary>
		///     Gets or sets the type of the message.
		/// </summary>
		/// <value>The type of the message.</value>
		public MessageType MessageType { get; set; }

		/// <summary>
		///     Returns the <see cref="T:System.TypeCode" /> for this instance.
		/// </summary>
		/// <returns>
		///     The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that
		///     implements this interface.
		/// </returns>
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt16;
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting
		///     information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>A Boolean value equivalent to the value of this instance.</returns>
		public bool ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent Unicode character using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>A Unicode character equivalent to the value of this instance.</returns>
		public char ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
		public sbyte ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
		public byte ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
		public short ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
		public ushort ToUInt16(IFormatProvider provider)
		{
			return (ushort) ((ushort) MessageIdentifier << 8 | (ushort) MessageType);
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
		public int ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
		public uint ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
		public long ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific
		///     formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
		public ulong ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent single-precision floating-point number using the specified
		///     culture-specific formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
		public float ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent double-precision floating-point number using the specified
		///     culture-specific formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
		public double ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified
		///     culture-specific formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
		public decimal ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified
		///     culture-specific formatting information.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
		public DateTime ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(ToUInt16(provider));
		}

		/// <summary>
		///     Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public string ToString(IFormatProvider provider)
		{
			return Convert.ToString(ToUInt16(provider));
		}

		/// <summary>
		///     Converts the value of this instance to an <see cref="T:System.Object" /> of the specified
		///     <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting
		///     information.
		/// </summary>
		/// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted.</param>
		/// <param name="provider">
		///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
		///     culture-specific formatting information.
		/// </param>
		/// <returns>
		///     An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is
		///     equivalent to the value of this instance.
		/// </returns>
		public object ToType(Type conversionType, IFormatProvider provider)
		{
			if (conversionType == typeof (byte[]))
			{
				ushort s = Convert.ToUInt16(this);

				var bytes = new byte[2];
				bytes[0] = Convert.ToByte((s << 8) & 0xFF);
				bytes[1] = Convert.ToByte(s & 0xFF);

				return bytes;
			}
			return ((IConvertible) ToUInt16(provider)).ToType(conversionType, provider);
		}
	}
}