using System;
using System.Collections.Generic;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
    public abstract class CommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse> 
        : ICommandProcessor<TIdentifier, TStore, TCommand, TCommandWithResponse, TResponse>
        where TIdentifier : struct, IConvertible
        where TStore : class
        where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore> 
        where TCommandWithResponse : class, ICommandWithResponse<TIdentifier, TStore, TResponse>, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>  
        where TResponse : class, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>, new()
    {
        private bool _isProcessingCommands = false;
        private Thread _commandProcessingThread = null;
        private readonly AutoResetEvent _processCommandsResetEvent = new AutoResetEvent(false);
        private readonly AutoResetEvent _processedCommandsResetEvent = new AutoResetEvent(false);
        private readonly object _commandLock = new object();
        private readonly Queue<ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>>> _commandQueue 
            = new Queue<ICommandTransaction<TIdentifier, TStore, ICommand<TIdentifier, TStore>>>();

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
        /// Sends the command with a response.
        /// </summary>
        /// <typeparam name="TRsp">The type of the RSP.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public TRsp SendCommand<TRsp>(TCommandWithResponse command) where TRsp : class, TResponse, IResponse<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
        {
            try
            {
                lock (_commandLock)
                {
                    // create the transaction
                    var transaction =
                        new CommandWithResponseTransaction<TIdentifier, TStore, TCommandWithResponse, TResponse>(command);

                    // enqueue it for processing
                    _commandQueue.Enqueue(transaction);

                    // let the processing thread know to do some work
                    _processCommandsResetEvent.Set();
                }

                // wait for the background worker to process the command
                var signaled = _processedCommandsResetEvent.WaitOne(this.CommandTimeout);
                if (!signaled)
                    throw new TimeoutException();
            }
            finally
            {
                lock (_commandLock)
                {
                    // TODO
                }
            }

            // TODO
            return null;
        }

        /// <summary>
        /// Sends the command without a response.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommand(TCommand command)
        {
            try
            {
                lock (_commandLock)
                {
                    // create the transaction
                    var transaction = new CommandTransaction<TIdentifier, TStore, TCommand>(command);

                    // enqueue it for processing
                    _commandQueue.Enqueue(transaction);

                    // let the processing thread know to do some work
                    _processCommandsResetEvent.Set();
                }

                var signaled = _processedCommandsResetEvent.WaitOne(this.CommandTimeout);
                if (!signaled)
                    throw new TimeoutException();
            }
            finally
            {
                lock (_commandLock)
                {
                    
                }
            }

            // TODO
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

                // TODO
            }
        }
    }
}
