// ***********************************************************************
// Assembly         : CommandProcessor.Buffer.UnitTests
// Author           : Thomas
// Created          : 03-02-2014
//
// Last Modified By : Thomas
// Last Modified On : 03-02-2014
// ***********************************************************************
// <copyright file="IResponse.cs" company="Veldy.net">
//     Copyright (c) Veldy.net. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Interface IResponse
	/// </summary>
	interface IResponse : IResponse<Identifier, byte[]>, IMessage
	{
		/// <summary>
		/// Gets or sets the length of the response.
		/// </summary>
		/// <value>The length of the response.</value>
		int ResponseLength { get; set; }
	}
}
