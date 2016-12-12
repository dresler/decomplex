using decomplex.Extensions;

namespace decomplex.ChainOfResponsibility
{
    public abstract class MessageHandlerWithResponseBase<TMessage, TResponse> : IMessageHandlerWithResponseConstruction<TMessage,TResponse>
    {
        private IMessageHandlerWithResponse<TMessage, TResponse> _nextHandler;

        protected MessageHandlerWithResponseBase()
        {
            TryToReadOrderIndex();
        }

        private void TryToReadOrderIndex()
        {
            var chainOfResponsibilityOrderAttribute = this.FindAttribute<ChainOfResponsibilityOrderIndexAttribute>();
            if (chainOfResponsibilityOrderAttribute != null)
            {
                OrderIndex = chainOfResponsibilityOrderAttribute.Index;
            }
        }

        public TResponse Handle(TMessage message)
        {
            TResponse response;

            if (TryHandle(message, out response))
            {
                return response;
            }

            if (_nextHandler == null) return default(TResponse);

            return _nextHandler.Handle(message);
        }

        protected abstract bool TryHandle(TMessage message, out TResponse response);

        public IMessageHandlerWithResponseConstruction<TMessage, TResponse> RegisterNext(IMessageHandlerWithResponseConstruction<TMessage, TResponse> handlerNext)
        {
            _nextHandler = handlerNext;
            return handlerNext;
        }

        public int OrderIndex { get; private set; }
    }
}