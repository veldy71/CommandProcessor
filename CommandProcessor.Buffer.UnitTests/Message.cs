// ***********************************************************************
// Assembly         : CommandProcessor.Buffer.UnitTests
// Author           : Thomas
// Created          : 03-02-2014
//
// Last Modified By : Thomas
// Last Modified On : 03-02-2014
// ***********************************************************************
// <copyright file="Message.cs" company="Veldy.net">
//     Copyright (c) Veldy.net. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class Message.
	/// </summary>
	abstract class Message : IMessage<Identifier, byte[]>
	{
		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
		public IKey<Identifier, byte[]> Key { get; protected set; }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public abstract byte[] Store { get; }

		/// <summary>
		/// Gets the byte array.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="length">The length.</param>
		/// <returns>System.Byte[][].</returns>
		protected byte[] GetByteArray(int index, int length)
		{
			var value = new byte[length];
			System.Buffer.BlockCopy(Store, index, value, 0, length);
			return value;
		}

		/// <summary>
		/// Sets the byte array.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="array">The array.</param>
		protected void SetByteArray(int index, byte[] array)
		{
			System.Buffer.BlockCopy(array, 0, Store, index, array.Length);
		}
	}
}
