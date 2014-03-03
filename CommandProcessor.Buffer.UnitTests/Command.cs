// ***********************************************************************
// Assembly         : CommandProcessor.Buffer.UnitTests
// Author           : Thomas
// Created          : 03-02-2014
//
// Last Modified By : Thomas
// Last Modified On : 03-02-2014
// ***********************************************************************
// <copyright file="Command.cs" company="Veldy.net">
//     Copyright (c) Veldy.net. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace CommandProcessor.Buffer.UnitTests
{
	/// <summary>
	/// Class Command.
	/// </summary>
	abstract class Command : Message, ICommand
	{
		/// <summary>
		/// Gets the commandlength.
		/// </summary>
		/// <value>The commandlength.</value>
		public virtual int CommandLength { get { return 2; } }
	}
}
