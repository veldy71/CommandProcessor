// ***********************************************************************
// Assembly         : CommandProcessor.Buffer.UnitTests
// Author           : Thomas
// Created          : 03-02-2014
//
// Last Modified By : Thomas
// Last Modified On : 03-02-2014
// ***********************************************************************
// <copyright file="ICommandWithResponse.cs" company="Veldy.net">
//     Copyright (c) Veldy.net. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Interface ICommandWithResponse
	/// </summary>
	interface ICommandWithResponse<out TResponse> : ICommandWithResponse<Identifier, byte[], TResponse>, ICommand
		where TResponse : class, IResponse, IResponse<Identifier, byte[]>, IMessage, IMessage<Identifier, byte[]>
	{
	}
}
