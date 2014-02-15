using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class Key.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore"></typeparam>
	public abstract class Key<TIdentifier, TStore> : IKey<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible
		where TStore : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Key{TIdentifier, TStore}"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
		protected Key(TIdentifier identifier)
		{
			Identifier = identifier;
		}

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
		/// </returns>
		public abstract int CompareTo(TStore other);

		/// <summary>
		/// Gets the target.
		/// </summary>
		/// <value>
		/// The target.
		/// </value>
		public TIdentifier Identifier { get; private set; }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>
		/// The store.
		/// </value>
		public abstract TStore Store { get; } 
	}
}
