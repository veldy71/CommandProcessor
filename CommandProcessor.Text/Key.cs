using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Veldy.Net.CommandProcessor.Text
{
	class Key<TIdentifier> : IKey<TIdentifier, string>
		where TIdentifier : struct, IConvertible
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Key{TTarget}"/> class.
		/// </summary>
		/// <param name="identifier">The target.</param>
		/// <exception cref="System.ArgumentNullException">target</exception>
		public Key(TIdentifier identifier)
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
		public int CompareTo(string other)
		{
			return String.Compare(this.Store, other.Length < this.Store.Length 
				? other 
				: other.Substring(0, this.Store.Length), StringComparison.Ordinal);
		}

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
		public virtual string Store
		{
			get
			{
				var type = typeof (TIdentifier);
				var memInfo = type.GetMember(this.Identifier.ToString(CultureInfo.InvariantCulture));
				var attributes = memInfo[0].GetCustomAttributes(typeof (EnumTextAttribute),
					false);
				var store = ((EnumTextAttribute) attributes[0]).Text;

				return store;
			}
		}
	}
}
