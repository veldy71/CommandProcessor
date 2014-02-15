using System;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	/// Interface ICommandProcessor
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	/// <typeparam name="TCommandWithResponse">The type of the t command with response.</typeparam>
	/// <typeparam name="TResponse">The type of the t response.</typeparam>
	public interface ICommandProcessor<in TIdentifier, TStore, in TCommand, in TCommandWithResponse, in TResponse> : IDisposable
		where TIdentifier : struct, IConvertible
        where TStore : class
        where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>  
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
	{
        /// <summary>
        /// The period of time to wait before checking for work.
        /// </summary>
        int CommandWait { get; }

        /// <summary>
        /// Gets the command timeout.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        int CommandTimeout { get; }

        /// <summary>
        /// Gets a value indicating whether [is processing messages].
        /// </summary>
        /// <value>
        /// <c>true</c> if [is processing messages]; otherwise, <c>false</c>.
        /// </value>
        bool IsProcessingMessages { get; }

		/// <summary>
		/// Sends the command.
		/// </summary>
		/// <typeparam name="TRsp">The type of the t RSP.</typeparam>
		/// <param name="command">The command.</param>
		/// <returns>``0.</returns>
		TRsp SendCommand<TRsp>(ICommandWithResponse<TIdentifier, TStore, TRsp> command)
			where TRsp : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new();

        /// <summary>
        /// Sends the command without a response.
        /// </summary>
        /// <param name="command">The command.</param>
	    void SendCommand(TCommand command);

        /// <summary>
        /// Starts the command processing.
        /// </summary>
	    void StartProcessing();

        /// <summary>
        /// Stops the command processing.
        /// </summary>
	    void StopProcessing();
	}
}
