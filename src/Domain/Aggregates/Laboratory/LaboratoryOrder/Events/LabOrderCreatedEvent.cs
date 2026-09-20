using SharedKernel.DomainEvent;

namespace Domain.Aggregates.Laboratory.LaboratoryOrder.Events
{
    public record LabOrderCreatedEvent(Guid LabOrderId, Guid PatientId, Guid AppointmentId) : IDomainEvent
    {
        public string DomainEventName => DomainEventsNames.LabOrderEventNames.LabOrderCreated;
    }
}