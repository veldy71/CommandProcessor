using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
    public abstract class CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse> 
        : ICommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TStore : class
        where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore> 
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>  
        where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
    {
        private bool _isProcessingCommands = false;
        private Thread _commandProcessingThread = null;
        private readonly AutoResetEvent _processCommandsResetEvent = new AutoResetEvent(false);
        private readonly object _commandLock = new object();
        private readonly Queue<ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>>> _commandQueue
            = new Queue<ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>>>();

        /// <summary>
        /// Gets or sets the push command with no response.
        /// </summary>
        /// <value>
        /// The push command with no response.
        /// </value>
        private Action<TStore> PushCommandWithNoResponse { get; set; }

        /// <summary>
        /// Gets or sets the push command with response.
        /// </summary>
        /// <value>
        /// The push command with response.
        /// </value>
        private Func<TStore, int, TStore> PushCommandWithResponse { get; set; } 

        /// <summary>
        /// Gets the command wait time in milliseconds.
        /// </summary>
        /// <value>
        /// The command wait.
        /// </value>
        public virtual int CommandWait { get { return 100; } }

        /// <summary>
        /// Gets the command timeout.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        public virtual int CommandTimeout { get { return 10000; } }

        /// <summary>
        /// Gets a value indicating whether [is processing messages].
        /// </summary>
        /// <value>
        /// <c>true</c> if [is processing messages]; otherwise, <c>false</c>.
        /// </value>
        public bool IsProcessingMessages { get { return _isProcessingCommands; } }

        /// <summary>
        /// Setups the push command functions.
        /// </summary>
        /// <param name="pushCommandWithNoResponse">The push command with no response.</param>
        /// <param name="pushCommandWithResponse">The push command with response.</param>
        /// <exception cref="System.ArgumentNullException">
        /// pushCommandWithNoResponse
        /// or
        /// pushCommandWithResponse
        /// </exception>
        protected void SetupPushCommandFunctions(Action<TStore> pushCommandWithNoResponse, Func<TStore, int, TStore> pushCommandWithResponse)
        {
            if (pushCommandWithNoResponse == null)
                throw new ArgumentNullException("pushCommandWithNoResponse");

            if (pushCommandWithResponse == null)
                throw new ArgumentNullException("pushCommandWithResponse");

            PushCommandWithNoResponse = pushCommandWithNoResponse;
            PushCommandWithResponse = pushCommandWithResponse;
        }

        /// <summary>
        /// Sends the command with a response.
        /// </summary>
        /// <typeparam name="TCmd">The type of the command.</typeparam>
        /// <typeparam name="TRsp">The type of the RSP.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.TimeoutException"></exception>
        public TRsp SendCommand<TCmd, TRsp>(TCmd command) 
            where TCmd : class, TCommandWithResponse, ICommandWithResponse<TIdentifier, TStore, TRsp>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>  
            where TRsp : class, TResponse, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
        {
            // create the transaction
            var transaction =
                new CommandWithResponseTransaction<TIdentifier, TStore, ICommandWithResponse<TIdentifier, TStore, TRsp>, TRsp>(command);

            try
            {
                lock (_commandLock)
                {
                    // enqueue it for processing
                    _commandQueue.Enqueue(transaction);

                    // let the processing thread know to do some work
                    _processCommandsResetEvent.Set();
                }

                // wait for the background worker to process the command
                var signaled = transaction.ResetEvent.WaitOne(this.CommandTimeout);
                if (!signaled)
                    throw new TimeoutException();

                // if there was an exception on the background thread, throw it here
                if (transaction.Exception != null)
                    throw transaction.Exception;

                return transaction.Response;
            }
            finally
            {
                lock (_commandLock)
                {
                    transaction.Dispose();
                }
            }
        }

        /// <summary>
        /// Sends the command without a response.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommand(TCommand command)
        {
            // create the transaction
            var transaction = new CommandTransaction<TIdentifier, TStore, TCommand>(command);

            try
            {
                lock (_commandLock)
                {
                    // enqueue it for processing
                    _commandQueue.Enqueue(transaction);

                    // let the processing thread know to do some work
                    _processCommandsResetEvent.Set();
                }

                var signaled = transaction.ResetEvent.WaitOne(this.CommandTimeout);
                if (!signaled)
                    throw new TimeoutException();

                // if there was an exception on the background thread, throw it here
                if (transaction.Exception != null)
                    throw transaction.Exception;
            }
            finally
            {
                lock (_commandLock)
                {
                    transaction.Dispose();
                }
            }
        }

        /// <summary>
        /// Starts the command processing.
        /// </summary>
        public void StartCommandProcessing()
        {
            if (!IsProcessingMessages)
            {
                _isProcessingCommands = true;
                _commandProcessingThread = new Thread(ProcessCommands) { Priority = ThreadPriority.AboveNormal };
                _commandProcessingThread.Start();
            }
        }

        /// <summary>
        /// Stops the command processing.
        /// </summary>
        public void StopCommandProcessing()
        {
            if (IsProcessingMessages)
            {
                _isProcessingCommands = false;
                var signaled = _commandProcessingThread.Join(this.CommandWait);
                if (!signaled)
                    _commandProcessingThread.Abort();

                _commandProcessingThread = null;
            }
        }

        /// <summary>
        /// Processes the commands.
        /// </summary>
        private void ProcessCommands()
        {
            while (this.IsProcessingMessages)
            {
                _processCommandsResetEvent.WaitOne(this.CommandWait);

                ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>> transaction = null;

                lock (_commandLock)
                {
                    if (_commandQueue.Any())
                        transaction = _commandQueue.Dequeue();
                }

                // process the transaction
                if (transaction != null)
                {
                    try
                    {
                        var commandStore = transaction.Command.Store;
                        if (transaction.HasResponse)
                        {
                            var commandWithResponseTransaction = ((ICommandWithResponseTransaction
                                <TIdentifier, TStore, ICommandWithResponse<TIdentifier, TStore, TResponse>,
                                    TResponse>) transaction);

                            var responseStore = PushCommandWithResponse(commandStore, commandWithResponseTransaction.CommandWithResponse.ResponseLength);
                            commandWithResponseTransaction.SetResponseStore(responseStore);
                        }
                        else
                        {
                            PushCommandWithNoResponse(commandStore);
                        }

                        transaction.SetInvactive();
                    }
                    catch (Exception e)
                    {
                        transaction.SetException(e);
                    }
                }
            }
        }
    }
}
