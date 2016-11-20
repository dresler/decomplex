using System;
using System.Collections.Generic;
using System.Linq;

namespace decomplex.ChainOfResponsibility
{
    /// <summary>
    /// Base class for a wrapper of chain of responsibility pattern.
    /// </summary>
    /// <typeparam name="TMessage">Type of handled message.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    public abstract class ChainOfResponsibilityBase<TMessage, TResponse>
    {
        protected IMessageHandlerWithResponse<TMessage, TResponse> FirstChainHandler;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="handlers">Handlers to construct chain of responsibility.</param>
        protected ChainOfResponsibilityBase(IEnumerable<IMessageHandlerWithResponseConstruction<TMessage,TResponse>> handlers)
        {
            if (handlers == null) throw new ArgumentNullException(nameof(handlers));
            if (!handlers.Any()) throw new ArgumentException("There has to be at least one handler.");

            var orderedHandlers = OrderHandlers(handlers);
            RegisterHandlers(orderedHandlers);
            FirstChainHandler = orderedHandlers.First();
        }

        /// <summary>
        /// Handles message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Response.</returns>
        public TResponse Handle(TMessage message)
        {
            return FirstChainHandler.Handle(message);
        }

        private IEnumerable<IMessageHandlerWithResponseConstruction<TMessage, TResponse>> OrderHandlers(IEnumerable<IMessageHandlerWithResponseConstruction<TMessage, TResponse>> handlers)
        {
            return handlers.OrderBy(handler => handler.OrderIndex).ToList();
        }

        private void RegisterHandlers(IEnumerable<IMessageHandlerWithResponseConstruction<TMessage, TResponse>> orderedHandlers)
        {
            var firstHandler = orderedHandlers.First();
            var otherHandlers = orderedHandlers.Skip(1);

            otherHandlers.Aggregate(firstHandler, (current, nextHandler) => current.RegisterNext(nextHandler));
        }
    }
}