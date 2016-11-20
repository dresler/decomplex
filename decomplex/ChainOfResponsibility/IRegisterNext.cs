namespace decomplex.ChainOfResponsibility
{
    /// <summary>
    /// Registration of next handler in chain of responsibility.
    /// </summary>
    /// <typeparam name="TMessage">Type of message.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public interface IRegisterNext<TMessage, TResponse>
    {
        /// <summary>
        /// Registers next handler for current one.
        /// </summary>
        /// <param name="handlerNext">Next registering handler.</param>
        /// <returns>Next registered handler.</returns>
        IMessageHandlerWithResponseConstruction<TMessage, TResponse> RegisterNext(IMessageHandlerWithResponseConstruction<TMessage, TResponse> handlerNext);
    }
}