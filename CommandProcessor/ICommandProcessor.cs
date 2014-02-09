using System;

namespace Veldy.Net.CommandProcessor
{
	public interface ICommandProcessor<in TIdentifier, TStore, in TCommand, in TCommandResponse, in TResponse>
		where TIdentifier : struct, IConvertible
        where TStore : class
        where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
        where TCommandResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>  
		where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
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
        /// Sends a command with a response.
        /// </summary>
        /// <typeparam name="TRsp"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
	    TRsp SendCommand<TRsp>(TCommandResponse command)
	        where TRsp : class, TResponse, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>;

        /// <summary>
        /// Sends the command without a response.
        /// </summary>
        /// <param name="command">The command.</param>
	    void SendCommand(TCommand command);

        /// <summary>
        /// Starts the command processing.
        /// </summary>
	    void StartCommandProcessing();

        /// <summary>
        /// Stops the command processing.
        /// </summary>
	    void StopCommandProcessing();
	}
}
