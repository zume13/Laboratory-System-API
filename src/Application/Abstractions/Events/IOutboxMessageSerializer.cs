using SharedKernel.DomainEvent;

namespace Application.Abstractions.Events
{
    public interface IOutboxMessageSerializer
    {
        string Serialize(IDomainEvent domainEvent);
        object Deserialize(string content, Type type);
    }
}