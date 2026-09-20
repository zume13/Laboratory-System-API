using SharedKernel.DomainEvent;

namespace Domain.Aggregates.Appointment.Events
{
    public record AppointmentCancelledEvent(Guid AppointmentId, Guid PatientId) : IDomainEvent
    {
        public string DomainEventName => DomainEventsNames.AppointmentEventNames.AppointmentCancelled;
    }
}