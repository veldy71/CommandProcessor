// ***********************************************************************
// Assembly         : CommandProcessor.Buffer.UnitTests
// Author           : Thomas
// Created          : 03-02-2014
//
// Last Modified By : Thomas
// Last Modified On : 03-02-2014
// ***********************************************************************
// <copyright file="Response.cs" company="Veldy.net">
//     Copyright (c) Veldy.net. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class Response.
	/// </summary>
	class Response : Message, IResponse
	{
		private byte[] _store = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Response"/> class.
		/// </summary>
		public Response()
		{
			ResponseLength = 2;
		}

		/// <summary>
		/// Sets the store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <exception cref="System.ArgumentNullException">store</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">store</exception>
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

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public override byte[] Store
		{
			get
			{
				if (_store == null || _store.Length != ResponseLength)
				{
					// allocate a new store copy all but the id
					var old = _store;
					_store = new byte[ResponseLength];

					if (old != null)
						System.Buffer.BlockCopy(old, ResponseLength, _store, ResponseLength, Math.Min(old.Length, ResponseLength - 2));

					// copy the ID
					var id = BitConverter.GetBytes(Convert.ToUInt16(Key.Identifier));
					System.Buffer.BlockCopy(id, 0, _store, 0, id.Length);
				}

				return _store;
			}
		}

		/// <summary>
		/// Gets or sets the length of the response.
		/// </summary>
		/// <value>The length of the response.</value>
		public int ResponseLength { get; set; }
	}
}
