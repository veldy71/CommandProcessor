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
		private byte[] _store = new byte[0];

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

			_store = store;
		}

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public override byte[] Store { get { return _store; } }

		/// <summary>
		/// Gets or sets the length of the response.
		/// </summary>
		/// <value>The length of the response.</value>
		public int ResponseLength { get; set; }
	}
}
