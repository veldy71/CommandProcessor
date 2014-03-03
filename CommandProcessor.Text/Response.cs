using System;

namespace Veldy.Net.CommandProcessor.Text
{
	/// <summary>
	///     Class Response.
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	public abstract class Response<TIdentifier> : Message<TIdentifier>, IResponse<TIdentifier>
		where TIdentifier : struct, IConvertible, IComparable<string> 
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Response{TIdentifier}" /> class.
		/// </summary>
		protected Response()
			: base(default(TIdentifier))
		{
		}

		/// <summary>
		///     Sets the store.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <exception cref="System.ArgumentNullException">store</exception>
		public void SetStore(string store)
		{
			if (store == null)
				throw new ArgumentNullException("store");

			Store = store;
		}
	}
}