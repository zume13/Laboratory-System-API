using Application.Abstractions.Events;
using SharedKernel.DomainEvent;
using SharedKernel.Shared;

namespace Infrastructure.Events
{
    public class DomainEventTypeRegistry : IDomainEventTypeRegistry
    {
       // TODO event mapping
        private readonly Dictionary<string, Type> _map = new();

        public ResultT<Type> Resolve(string eventName)
        {
            if (_map.TryGetValue(eventName, out var type))
                return type;

            return Error.Problem("DomainEvent.UnknownType", $"No type registered for event name '{eventName}'.");
        }
    }
}