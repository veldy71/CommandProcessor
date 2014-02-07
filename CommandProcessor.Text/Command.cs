using System;

namespace Veldy.Net.CommandProcessor.Text
{
    public abstract class Command<TIdentifier, TResponse> : Message<TIdentifier>, ICommand<TIdentifier, TResponse>
        where TResponse : class, IResponse<TIdentifier>, IResponse<TIdentifier, string>, IMessage<TIdentifier>, IMessage<TIdentifier, string>, new()
        where TIdentifier : struct, IConvertible
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Command{TIdentifier, TResponse}"/> class.
		/// </summary>
		/// <param name="identifier">The identifier.</param>
        protected Command(TIdentifier identifier) : base(identifier)
        { }

		/// <summary>
		/// Gets the store.
		/// </summary>
		/// <value>
		/// The store.
		/// </value>
		public override string Store
		{
			get
			{
				var store = base.Store;

				PopulateStore(ref store);

				return store;
			}
		}

		/// <summary>
		/// Populates the store.
		/// </summary>
		/// <param name="store">The store.</param>
		protected abstract void PopulateStore(ref string store);

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        public TResponse CreateResponse(string store)
        {
			var response = new TResponse();

            if (response.Key.CompareTo(store) == 0)
            {
                response.SetStore(store);

                return response;
            }

            return null;
        }
    }
}
