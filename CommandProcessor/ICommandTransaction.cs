using System;
using System.Threading;

namespace Veldy.Net.CommandProcessor
{
	/// <summary>
	///     Interface ICommandTransaction
	/// </summary>
	/// <typeparam name="TIdentifier">The type of the t identifier.</typeparam>
	/// <typeparam name="TStore">The type of the t store.</typeparam>
	/// <typeparam name="TCommand">The type of the t command.</typeparam>
	public interface ICommandTransaction<TIdentifier, TStore, out TCommand> : IDisposable
		where TCommand : class, ICommand<TIdentifier, TStore>, IMessage<TIdentifier, TStore>
		where TIdentifier : struct, IConvertible, IComparable<TStore>
		where TStore : class
	{
		/// <summary>
		///     Gets the command.
		/// </summary>
		/// <value>
		///     The command.
		/// </value>
		TCommand Command { get; }

		/// <summary>
		///     Gets a value indicating whether [is active].
		/// </summary>
		/// <value>
		///     <c>true</c> if [is active]; otherwise, <c>false</c>.
		/// </value>
		bool IsActive { get; }

		/// <summary>
		///     Gets the reset event.
		/// </summary>
		/// <value>
		///     The reset event.
		/// </value>
		ManualResetEvent ResetEvent { get; }

		/// <summary>
		///     Gets the exception.
		/// </summary>
		/// <value>
		///     The exception.
		/// </value>
		Exception Exception { get; }

		/// <summary>
		///     Gets a value indicating whether [has response].
		/// </summary>
		/// <value>
		///     <c>true</c> if [has response]; otherwise, <c>false</c>.
		/// </value>
		bool HasResponse { get; }

		/// <summary>
		///     Sets the invactive.
		/// </summary>
		void SetInvactive();

		/// <summary>
		///     Sets the exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		void SetException(Exception exception);
	}
}