using Domain.Aggregates.Appointment;
using Domain.Aggregates.Appointment.Enums;
using Domain.Aggregates.AppointmentSlot;
using SharedKernel.Shared;

namespace Domain.Services
{
    public static class AppointmentBookingService
    {
        // The one place the capacity invariant and the Appointment creation are
        // guaranteed to happen together — the handler cannot create an Appointment
        // without going through Slot.Reserve() first.
        public static ResultT<Appointment> Book(
            AppointmentSlot slot,
            Guid patientId,
            Guid testCategoryId,
            BookingChannel channel)
        {
            var reserveResult = slot.Reserve();
            if (reserveResult.IsFailure)
                return reserveResult.Error;

            return Appointment.Create(patientId, slot.Id, testCategoryId, channel);
        }

        public static Result Cancel(Appointment appointment, AppointmentSlot slot)
        {
            var cancelResult = appointment.Cancel();
            if (cancelResult.IsFailure)
                return cancelResult.Error;

            return slot.Release();
        }

        public static Result MarkNoShow(Appointment appointment, AppointmentSlot slot)
        {
            var noShowResult = appointment.MarkNoShow();
            if (noShowResult.IsFailure)
                return noShowResult.Error;

            return slot.Release();
        }
    }
}
