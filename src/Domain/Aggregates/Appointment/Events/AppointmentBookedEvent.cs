using SharedKernel.DomainEvent;

namespace Domain.Aggregates.Appointment.Events
{
    public record AppointmentBookedEvent(Guid AppointmentId, Guid PatientId, Guid AppointmentSlotId) : IDomainEvent
    {
        public string DomainEventName => DomainEventsNames.AppointmentEventNames.AppointmentBooked;
    }
}