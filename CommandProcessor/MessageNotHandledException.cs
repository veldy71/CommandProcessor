using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Class MessageNotHandledException.
	/// </summary>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	public class MessageNotHandledException<TStore> : Exception
		where TStore : class
	{
		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>The store.</value>
		public TStore Store { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageNotHandledException{TStore}"/> class.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <exception cref="System.ArgumentNullException">store</exception>
		public MessageNotHandledException(TStore store)
		{
			if (store == null)
				throw new ArgumentNullException("store");

			Store = store;
		}
	}
}
