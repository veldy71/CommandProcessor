using System;
using System.Diagnostics;

namespace Veldy.Net.CommandProcessor.Text
{
    class Key<TTarget> : IKey<TTarget, string>
        where TTarget : class, IMessage, IMessage<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Key{TTarget}"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <exception cref="System.ArgumentNullException">target</exception>
        public Key(TTarget target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            Target = target;
        }

        /// <summary>
        /// Gets the delimeter.
        /// </summary>
        /// <value>
        /// The delimeter.
        /// </value>
        protected virtual char Delimeter { get { return ' '; } }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(string other)
        {
            var rhsParts = other.Split(Delimeter);

            Debug.Assert(Target.StoreParts.Length > 0);

            return rhsParts.Length > 0
                ? String.Compare(Target.StoreParts[0], rhsParts[0], StringComparison.Ordinal)
                : int.MaxValue;
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public TTarget Target { get; private set; }
    }
}
