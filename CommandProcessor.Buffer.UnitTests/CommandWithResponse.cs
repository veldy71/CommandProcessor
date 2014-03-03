// ***********************************************************************
// Assembly         : CommandProcessor.Buffer.UnitTests
// Author           : Thomas
// Created          : 03-02-2014
//
// Last Modified By : Thomas
// Last Modified On : 03-02-2014
// ***********************************************************************
// <copyright file="CommandWithResponse.cs" company="Veldy.net">
//     Copyright (c) Veldy.net. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class CommandWithResponse.
	/// </summary>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	abstract class CommandWithResponse<TResponse> : Command, ICommandWithResponse<TResponse>
		where TResponse : class, IResponse, IResponse<Identifier, byte[]>, IMessage, IMessage<Identifier, byte[]>, new()
	{
		/// <summary>
		/// Gets the length of the response.
		/// </summary>
		/// <value>The length of the response.</value>
		public abstract int ResponseLength { get; }

		/// <summary>
		/// Creates the response.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <returns>`0.</returns>
		public TResponse CreateResponse(byte[] store)
		{
			var response = ResponseHandler<Identifier, byte[]>.HandleResponse<ICommandWithResponse<TResponse>, TResponse>(this, store);

			if (response != null)
			{
				response.ResponseLength = ResponseLength;
				response.SetStore(store);
			}

			return response;
		}

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public override byte[] Store
		{
			get
			{
				var store = new byte[CommandLength];

				var idBytes = BitConverter.GetBytes(Convert.ToUInt16(Key.Identifier));
				store[0] = idBytes[0];
				store[1] = idBytes[1];

				SetupStore(store);

				return store;
			}
		}

		/// <summary>
		/// Setups the store.
		/// </summary>
		/// <param name="store">The store.</param>
		protected abstract void SetupStore(byte[] store);
	}
}
