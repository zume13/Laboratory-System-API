using Application.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.DomainEvent;
using System.Collections.Concurrent;

namespace Infrastructure.Events
{
    public class DomainEventsDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly ConcurrentDictionary<Type, Type> HandlerTypeCache = new();
        private static readonly ConcurrentDictionary<Type, Type> WrapperTypeCache = new();

        public DomainEventsDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                using var scope = _serviceProvider.CreateScope();

                var domainEventType = domainEvent.GetType();
                var handlerType = HandlerTypeCache.GetOrAdd(
                    domainEventType,
                    et => typeof(IDomainEventHandler<>).MakeGenericType(et));

                var handlers = scope.ServiceProvider.GetServices(handlerType);

                foreach (var handler in handlers)
                {
                    if (handler is null)
                        continue;

                    var wrapper = HandlerWrapper.Create(handler, domainEventType);
                    await wrapper.Handle(domainEvent, cancellationToken);
                }
            }
        }

        private abstract class HandlerWrapper
        {
            public abstract Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken);

            public static HandlerWrapper Create(object handler, Type domainEventType)
            {
                var wrapperType = WrapperTypeCache.GetOrAdd(
                    domainEventType,
                    et => typeof(HandlerWrapper<>).MakeGenericType(et));

                return (HandlerWrapper)Activator.CreateInstance(wrapperType, handler)!;
            }
        }

        private sealed class HandlerWrapper<T> : HandlerWrapper where T : IDomainEvent
        {
            private readonly IDomainEventHandler<T> _handler;

            public HandlerWrapper(object handler)
            {
                _handler = (IDomainEventHandler<T>)handler;
            }

            public override async Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
            {
                await _handler.Handle((T)domainEvent, cancellationToken);
            }
        }
    }
}