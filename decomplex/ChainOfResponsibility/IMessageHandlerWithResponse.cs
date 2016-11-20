namespace decomplex.ChainOfResponsibility
{
    /// <summary>
    /// Message handler with response.
    /// </summary>
    /// <typeparam name="TMessage">Type of handled message.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public interface IMessageHandlerWithResponse<in TMessage, out TResponse>
    {
        /// <summary>
        /// Handles message. 
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Response.</returns>
        TResponse Handle(TMessage message);
    }
}