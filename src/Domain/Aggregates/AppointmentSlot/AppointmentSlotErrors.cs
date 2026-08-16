using SharedKernel.Shared;

namespace Domain.Aggregates.AppointmentSlot
{
    public static class AppointmentSlotErrors
    {
        public static Error FullSlots => Error.Conflict("AppointmentSlot.FullSlots", "No available slots for the selected date and time.");
        public static Error NoBookedSlots => Error.Conflict("AppointmentSlot.NoBookedSlots", "No booked slots for the selected date and time.");
    }
}
