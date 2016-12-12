using System;
using System.Collections.Generic;
using System.Linq;
using decomplex.Mapper;

namespace decomplex
{
    public abstract class CompositeHandlerBase<TData, THandler>
        where THandler : IHandler<TData>
    {
        private readonly IEnumerable<THandler> _handlers;

        protected CompositeHandlerBase(IEnumerable<THandler> handlers)
        {
            if (handlers == null) throw new ArgumentNullException(nameof(handlers));
            if (!handlers.Any()) throw new ArgumentException("No handlers passed to the composite handler.");

            _handlers = handlers;
        }

        public void Handle(TData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var handlersFor = _handlers.Where(h => h.IsFor(data)).ToList();

            if (!handlersFor.Any()) throw new ArgumentException("No handler found to handle data.", nameof(data));

            foreach (var handler in handlersFor)
            {
                handler.Handle(data);
            }
        }
    }
}