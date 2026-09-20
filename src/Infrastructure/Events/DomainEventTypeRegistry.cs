using Application.Abstractions.Events;
using Domain.Aggregates.Appointment.Events;
using Domain.Aggregates.Laboratory.LaboratoryOrder.Events;
using SharedKernel.DomainEvent;
using SharedKernel.Shared;

namespace Infrastructure.Events
{
    public class DomainEventTypeRegistry : IDomainEventTypeRegistry
    {
        private readonly Dictionary<string, Type> _map = new()
        {
            [DomainEventsNames.LabOrderEventNames.LabOrderCreated] = typeof(LabOrderCreatedEvent),
            [DomainEventsNames.LabOrderEventNames.LabResultReleased] = typeof(LabResultReleasedEvent),
            [DomainEventsNames.AppointmentEventNames.AppointmentBooked] = typeof(AppointmentBookedEvent),
            [DomainEventsNames.AppointmentEventNames.AppointmentCancelled] = typeof(AppointmentCancelledEvent),
        };

        public ResultT<Type> Resolve(string eventName)
        {
            if (_map.TryGetValue(eventName, out var type))
                return type;

            return Error.Problem("DomainEvent.UnknownType", $"No type registered for event name '{eventName}'.");
        }
    }
}