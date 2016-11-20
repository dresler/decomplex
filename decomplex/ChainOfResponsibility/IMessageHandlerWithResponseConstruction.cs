namespace decomplex.ChainOfResponsibility
{
    /// <summary>
    /// Message handler with response construction.
    /// Extended IMessageHandlerWithResponse interface with support for creation of chain of responsibility.
    /// This interface should be used only during construction phase.
    /// </summary>
    /// <typeparam name="TMessage">Type of message.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public interface IMessageHandlerWithResponseConstruction<TMessage, TResponse> : IMessageHandlerWithResponse<TMessage, TResponse>, IRegisterNext<TMessage, TResponse>
    {
        /// <summary>
        /// Order index in chain of responsibility.
        /// </summary>
        int OrderIndex { get; }
    }
}