using SharedKernel.Shared;

namespace Domain.Aggregates.Appointment
{
    public static class AppointmentErrors
    {
        public static Error InvalidStatus => Error.Conflict("Appointment.InvalidStatus", "Invalid appointment status.");
        public static Error NotFound => Error.NotFound("Appointment.NotFound", "Appointment not found.");
    }
}
