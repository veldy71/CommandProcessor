using System;

namespace Veldy.Net.CommandProcessor
{
    class CommandTransaction<TIdentifier, TStore, TCommand>
        : ICommandTransaction<TIdentifier, TStore, TCommand> 
        where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
        where TIdentifier : struct, IConvertible
        where TStore : class
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public TCommand Command { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [is active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is active]; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; protected set; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandTransaction{TIdentifier, TStore, TCommand}"/> class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">command</exception>
        public CommandTransaction(TCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            Command = command;
            IsActive = true;
            Exception = null;
        }

        /// <summary>
        /// Sets the invactive.
        /// </summary>
        public void SetInvactive()
        {
            IsActive = false;
        }

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void SetException(Exception exception)
        {
            IsActive = false;
            Exception = exception;
        }
    }
}
