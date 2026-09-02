using SharedKernel.Shared;

namespace Domain.Aggregates.AppointmentSlot
{
    public static class AppointmentSlotErrors
    {
        public static Error FullSlots => Error.Conflict("AppointmentSlot.FullSlots", "No available slots for the selected date and time.");
        public static Error NoBookedSlots => Error.Conflict("AppointmentSlot.NoBookedSlots", "No booked slots for the selected date and time.");

        public static Error NotFound(Guid id) => Error.NotFound("AppointmentSlot.NotFound", $"No appointment slot found with id '{id}'.");

        public static Error HasActiveBookings(Guid id) => Error.Conflict("AppointmentSlot.HasActiveBookings", $"Appointment slot '{id}' has active bookings and cannot be deleted.");
        public static Error CapacityBelowBookedCount => Error.Conflict("AppointmentSlot.CapacityBelowBookedCount", "Capacity cannot be reduced below the current number of bookings.");
        public static Error InvalidSelectedSlot => Error.Conflict("AppointmentSlot.InvalidSelectedSlot", "The selected appointment slot is invalid or unavailable.");
    }
}
