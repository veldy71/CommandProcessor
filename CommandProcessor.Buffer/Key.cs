using System;

namespace Veldy.Net.CommandProcessor.Buffer
{
	/// <summary>
	/// Class Key.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public class Key<TIdentifier> : Key<TIdentifier, byte[]>
		where TIdentifier : struct, IConvertible
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Key{TTarget}" /> class.
		/// </summary>
		/// <param name="identifier">The target.</param>
		/// <exception cref="System.ArgumentNullException">target</exception>
		public Key(TIdentifier identifier)
			: base(identifier)
        { }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public override int CompareTo(byte[] other)
        {
	        if (other == null)
		        return int.MaxValue;

	        if (other.Length < this.Store.Length)
		        return this.Store[other.Length].CompareTo(0);

	        var v = 0;

	        for (var i = 0; v == 0 && i < this.Store.Length; i++)
		        v = this.Store[i].CompareTo(other[i]);

	        return v;
        }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>
		/// The store.
		/// </value>
		public override byte[] Store { get { return new[] {Convert.ToByte(this.Identifier)}; } }
    }
}
