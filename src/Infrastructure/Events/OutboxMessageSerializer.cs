using Application.Abstractions.Events;
using SharedKernel.DomainEvent;
using System.Text.Json;

namespace Infrastructure.Events
{
    public class OutboxMessageSerializer : IOutboxMessageSerializer
    {
        public string Serialize(IDomainEvent domainEvent) =>
            JsonSerializer.Serialize(domainEvent, domainEvent.GetType());

        public object Deserialize(string content, Type type) =>
            JsonSerializer.Deserialize(content, type)!;
    }
}