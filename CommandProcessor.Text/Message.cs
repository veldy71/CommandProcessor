﻿using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	///     Class Message.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public abstract class Message<TIdentifier> : IMessage<TIdentifier, string>
		where TIdentifier : struct, IConvertible, IComparable<string> 
	{
		private string _store = string.Empty;

		/// <summary>
		///     Initializes a new instance of the <see cref="Message{TEnumMessageId}" /> class.
		/// </summary>
		protected Message(TIdentifier identifier)
		{
			Key = new Key<TIdentifier>(identifier);
			_store = Key.Store;
		}

		/// <summary>
		///     Gets the delimeter.
		/// </summary>
		/// <value>
		///     The delimeter.
		/// </value>
		protected virtual char Delimeter
		{
			get { return ' '; }
		}

		/// <summary>
		///     Gets the store parts.
		/// </summary>
		/// <value>
		///     The store parts.
		/// </value>
		public string[] StoreParts
		{
			get { return Store.Split(Delimeter); }
		}

		/// <summary>
		///     Gets or sets the key.
		/// </summary>
		/// <value>
		///     The key.
		/// </value>
		public IKey<TIdentifier, string> Key { get; protected set; }

		/// <summary>
		///     Gets the store.
		/// </summary>
		/// <value>
		///     The store.
		/// </value>
		public virtual string Store
		{
			get { return _store; }
			protected set { _store = value ?? Key.Store; }
		}

		/// <summary>
		///     Compares to.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public virtual int CompareTo(string other)
		{
			return Key.CompareTo(other);
		}
	}
}