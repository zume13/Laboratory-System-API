using SharedKernel.Shared;

namespace Application.Abstractions.Events
{
    public interface IDomainEventTypeRegistry
    {
        ResultT<Type> Resolve(string eventName);
    }
}