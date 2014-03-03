// ***********************************************************************
// Assembly         : CommandProcessor.Buffer.UnitTests
// Author           : Thomas
// Created          : 03-02-2014
//
// Last Modified By : Thomas
// Last Modified On : 03-02-2014
// ***********************************************************************
// <copyright file="ICommand.cs" company="Veldy.net">
//     Copyright (c) Veldy.net. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Veldy.Net.CommandProcessor;

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Interface ICommand
	/// </summary>
	interface ICommand : ICommand<Identifier, byte[]>, IMessage
	{
		/// <summary>
		/// Gets the commandlength.
		/// </summary>
		/// <value>The commandlength.</value>
		int CommandLength { get; }
	}
}
