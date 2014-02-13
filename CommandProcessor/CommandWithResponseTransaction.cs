using System;

namespace Veldy.Net.CommandProcessor
{
	public class CommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>
        : CommandTransaction<TIdentifier, TStore, TCommandWithResponse>, ICommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
        where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
        where TStore : class
    {
        /// <summary>
        /// Gets the command with response.
        /// </summary>
        /// <value>
        /// The command with response.
        /// </value>
        public TCommandWithResponse CommandWithResponse { get; private set; }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public TResponse Response { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandWithResponseTransaction{TIdentifier, TStore, TCommand, TResponse}"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        public CommandWithResponseTransaction(TCommandWithResponse command)
            : base(command)
        {
            CommandWithResponse = command;
            Response = new TResponse();

			WaitingForResponse = false;
        }

        /// <summary>
        /// Gets a value indicating whether [has response].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has response]; otherwise, <c>false</c>.
        /// </value>
        public override bool HasResponse
        {
            get { return true; }
        }

        /// <summary>
        /// Sets the response store.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        public bool SetResponseStore(TStore store)
        {
            IsActive = false;

            if (Response.Key.CompareTo(store) == 0)
            {
                Response.SetStore(store);
                return true;
            }

            return false;
        }

		/// <summary>
		/// Gets a value indicating whether [waiting for response].
		/// </summary>
		/// <value><c>true</c> if [waiting for response]; otherwise, <c>false</c>.</value>
		public bool WaitingForResponse { get; private set; }

		/// <summary>
		/// Sets the waiting for response.
		/// </summary>
		public void SetWaitingForResponse()
		{
			if (this.Response == null)
				this.WaitingForResponse = true;
		}
    }
}
