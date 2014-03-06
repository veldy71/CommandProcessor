// ***********************************************************************
// Assembly         : CommandProcessor.Buffer.UnitTests
// Author           : Thomas
// Created          : 03-02-2014
//
// Last Modified By : Thomas
// Last Modified On : 03-02-2014
// ***********************************************************************
// <copyright file="Identifier.cs" company="Veldy.net">
//     Copyright (c) Veldy.net. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;


namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Struct Identifier
	/// </summary>
	struct Identifier : IConvertible, IComparable<byte[]>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Identifier" /> struct.
		/// </summary>
		/// <param name="messageIdentifier">The message identifier.</param>
		/// <param name="messageType">Type of the message.</param>
		public Identifier(MessageIdentifier messageIdentifier, MessageType messageType) : this()
		{
			MessageIdentifier = messageIdentifier;
			MessageType = messageType;
		}

		/// <summary>
		/// Gets the message identifier.
		/// </summary>
		/// <value>The message identifier.</value>
		public MessageIdentifier MessageIdentifier { get; private set; }

		/// <summary>
		/// Gets the type of the message.
		/// </summary>
		/// <value>The type of the message.</value>
		public MessageType MessageType { get; private set; }

		/// <summary>
		/// Performs an explicit conversion from <see cref="System.Byte[][]"/> to <see cref="Identifier"/>.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		/// <exception cref="System.ArgumentNullException">value</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">value</exception>
		public static explicit operator Identifier(byte[] value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			if (value.Length < 2)
				throw new ArgumentOutOfRangeException("value");

			var bytes = BitConverter.ToUInt16(value, 0);

			return (Identifier) bytes;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="Identifier"/> to <see cref="System.Byte[][]"/>.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator byte[](Identifier identifier)
		{
			return BitConverter.GetBytes((ushort) identifier);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="System.UInt16"/> to <see cref="Identifier"/>.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator Identifier(ushort value)
		{
			var messageIdentifier = (MessageIdentifier) ((value >> 8) & 0xFF);
			var messageType = (MessageType) (value & 0xFF);

			return new Identifier(messageIdentifier, messageType);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="Identifier"/> to <see cref="System.UInt16"/>.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator ushort(Identifier identifier)
		{
			return Convert.ToUInt16(identifier);
		}

		/// <summary>
		/// Returns the <see cref="T:System.TypeCode" /> for this instance.
		/// </summary>
		/// <returns>The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that implements this interface.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt16;
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A Boolean value equivalent to the value of this instance.</returns>
		public bool ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A Unicode character equivalent to the value of this instance.</returns>
		public char ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
		public sbyte ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
		public byte ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
		public short ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
		public ushort ToUInt16(IFormatProvider provider)
		{
			return (ushort) (((ushort) MessageIdentifier << 8) | (ushort) MessageType);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
		public int ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
		public uint ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
		public long ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
		public ulong ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
		public float ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
		public double ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
		public decimal ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
		public DateTime ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(ToUInt16(provider));
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public string ToString(IFormatProvider provider)
		{
			return Convert.ToString(ToUInt16(provider));
		}

		/// <summary>
		/// Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this instance.</returns>
		public object ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible) ToUInt16(provider)).ToType(conversionType, provider);
		}

		/// <summary>
		/// Compares to.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns>System.Int32.</returns>
		/// <exception cref="System.ArgumentNullException">other</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">other</exception>
		public int CompareTo(byte[] other)
		{
			if (other == null)
				throw new ArgumentNullException("other");

			if (other.Length < 2)
				throw new ArgumentOutOfRangeException("other");

			var otherValue = (other[0] << 8) | other[1];
			var thisValue = ((byte) MessageIdentifier << 8) | (byte) MessageType;

			return thisValue.CompareTo(otherValue);
		}
	}
}
