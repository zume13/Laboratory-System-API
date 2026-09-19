
namespace SharedKernel.DomainEvent
{
    public static class DomainEventsNames
    {
        public static class UserEventNames
        {
            public const string UserCreated = "UserCreated";
        }

        public static class LabOrderEventNames
        {
            public const string LabOrderCreated = "LabOrder.Created";
            public const string LabResultReleased = "LabResult.Released";
        }

        public static class AppointmentEventNames
        {
            public const string AppointmentBooked = "Appointment.Booked";
            public const string AppointmentCancelled = "Appointment.Cancelled";
        }
    }
}
