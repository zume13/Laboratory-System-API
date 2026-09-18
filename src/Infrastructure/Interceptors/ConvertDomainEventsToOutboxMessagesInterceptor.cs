using Application.Abstractions.Events;
using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernel.Primitives;

namespace Infrastructure.Persistence.Interceptors
{
    public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
    {
        private readonly IOutboxMessageSerializer _serializer;

        public ConvertDomainEventsToOutboxMessagesInterceptor(IOutboxMessageSerializer serializer)
        {
            _serializer = serializer;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var outboxMessages = dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(entry => entry.Entity)
                .SelectMany(aggregate =>
                {
                    var domainEvents = aggregate.DomainEvents.ToList();

                    foreach (var domainEvent in domainEvents)
                        aggregate.RemoveDomainEvent(domainEvent);

                    return domainEvents;
                })
                .Select(domainEvent => new OutBoxMessage
                {
                    Id = Guid.NewGuid(),
                    EventName = domainEvent.DomainEventName,
                    Type = domainEvent.GetType().Name,
                    Content = _serializer.Serialize(domainEvent),
                    OccurredOn = DateTime.UtcNow,
                })
                .ToList();

            dbContext.Set<OutBoxMessage>().AddRange(outboxMessages);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}