using System;
using System.Globalization;
using System.Reflection;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	///     Class Key.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public class Key<TIdentifier> : Key<TIdentifier, string>
		where TIdentifier : struct, IConvertible, IComparable<string> 
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Key{TTarget}" /> class.
		/// </summary>
		/// <param name="identifier">The target.</param>
		/// <exception cref="System.ArgumentNullException">target</exception>
		public Key(TIdentifier identifier)
			: base(identifier)
		{
		}

		/// <summary>
		///     Gets the store.
		/// </summary>
		/// <value>
		///     The store.
		/// </value>
		public override string Store
		{
			get
			{
				Type type = typeof (TIdentifier);
				MemberInfo[] memInfo = type.GetMember(Identifier.ToString(CultureInfo.InvariantCulture));
				object[] attributes = memInfo[0].GetCustomAttributes(typeof (EnumTextAttribute),
					false);
				string store = ((EnumTextAttribute) attributes[0]).Text;

				return store;
			}
		}

		/// <summary>
		///     Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     A value that indicates the relative order of the objects being compared. The return value has the following
		///     meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This
		///     object is equal to <paramref name="other" />. Greater than zero This object is greater than
		///     <paramref name="other" />.
		/// </returns>
		public override int CompareTo(string other)
		{
			return String.Compare(Store, other.Length < Store.Length
				? other
				: other.Substring(0, Store.Length), StringComparison.Ordinal);
		}
	}
}