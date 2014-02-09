using System;

namespace Veldy.Net.CommandProcessor
{
    interface ICommandTransaction<TIdentifier, TStore, out TCommand>
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
        TCommand Command { get; }

        /// <summary>
        /// Gets a value indicating whether [is active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is active]; otherwise, <c>false</c>.
        /// </value>
        bool IsActive { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        Exception Exception { get; }

        /// <summary>
        /// Sets the invactive.
        /// </summary>
        void SetInvactive();

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void SetException(Exception exception);
    }
}
