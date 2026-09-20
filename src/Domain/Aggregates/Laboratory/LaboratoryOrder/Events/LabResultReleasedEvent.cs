using SharedKernel.DomainEvent;

namespace Domain.Aggregates.Laboratory.LaboratoryOrder.Events
{
    public record LabResultReleasedEvent(Guid LabOrderId, Guid PatientId, Guid RequestId, Guid TestCategoryId) : IDomainEvent
    {
        public string DomainEventName => DomainEventsNames.LabOrderEventNames.LabResultReleased;
    }
}