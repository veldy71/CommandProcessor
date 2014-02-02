﻿using System;

namespace Veldy.Net.CommandProcessor.Text
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumTextAttribute : Attribute
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTextAttribute"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public EnumTextAttribute(string text)
        {
            Text = text ?? string.Empty;
        }
    }
}
