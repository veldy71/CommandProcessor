namespace Veldy.Net.CommandProcessor.Text
{
    public interface IMessage : IMessage<string>
    {
        /// <summary>
        /// Gets the store parts.
        /// </summary>
        /// <value>
        /// The store parts.
        /// </value>
        string[] StoreParts { get; }
    }
}
