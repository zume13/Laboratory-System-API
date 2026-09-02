using Domain.Aggregates.Appointment.Enums;
using Domain.Aggregates.Communications.Enums;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Appointment
{
    public class Appointment : AggregateRoot
    {
        private readonly List<AppointmentReminder> _reminders = new();

        private Appointment() { }
        private Appointment(
            Guid id,
            Guid patientId,
            Guid appointmentSlotId,
            Guid testCategoryId,
            BookingChannel bookingChannel)
            : base(id)
        {
            PatientId = patientId;
            AppointmentSlotId = appointmentSlotId;
            TestCategoryId = testCategoryId;
            BookingChannel = bookingChannel;
            Status = AppointmentStatus.Booked;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid PatientId { get; private set; }

        public Guid AppointmentSlotId { get; private set; }

        public Guid TestCategoryId { get; private set; }

        public Guid? FulfillingLabRequestId { get; private set; }

        public AppointmentStatus Status { get; private set; }

        public BookingChannel BookingChannel { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? ConfirmedAt { get; private set; }

        public IReadOnlyCollection<AppointmentReminder> Reminders => _reminders.AsReadOnly();

        // NOTE: Booking touches two aggregates (Appointment + AppointmentSlot), so this
        // factory is meant to be called from a domain service that loads the slot,
        // calls slot.Reserve() first, and only creates the Appointment if that succeeds —
        // e.g. AppointmentBookingService in the application layer.
        public static ResultT<Appointment> Create(
            Guid patientId,
            Guid appointmentSlotId,
            Guid testCategoryId,
            BookingChannel bookingChannel)
        {
            if (patientId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(patientId));

            if (appointmentSlotId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(appointmentSlotId));

            if (testCategoryId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(testCategoryId));

            return new Appointment(
                Guid.NewGuid(),
                patientId,
                appointmentSlotId,
                testCategoryId,
                bookingChannel);
        }

        public Result Reserve()
        {
            if (Status == AppointmentStatus.Booked)
                return AppointmentErrors.InvalidStatus;

            Status = AppointmentStatus.Booked;
            ConfirmedAt = DateTime.UtcNow;

            return Result.Success();
        }

        // Also expected to be paired with slot.Release() in the same application-layer
        // transaction, since freeing capacity is the slot's responsibility, not this one's.
        public Result Cancel()
        {
            if (Status is AppointmentStatus.Completed or AppointmentStatus.Cancelled)
                return AppointmentErrors.InvalidStatus;

            Status = AppointmentStatus.Cancelled;

            return Result.Success();
        }

        public Result MarkNoShow()
        {
            if (Status is not (AppointmentStatus.Booked))
                return AppointmentErrors.InvalidStatus;

            Status = AppointmentStatus.NoShow;

            return Result.Success();
        }

        // Records that a LabRequest (a separate aggregate) now fulfills this appointment.
        // The LabRequest itself is created by the application layer via LabRequest.Create(...).
        public Result CompleteWithLabRequest(Guid labRequestId)
        {
            if (Status != AppointmentStatus.Booked)
                return AppointmentErrors.InvalidStatus;

            if (labRequestId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(labRequestId));

            FulfillingLabRequestId = labRequestId;
            Status = AppointmentStatus.Completed;

            return Result.Success();
        }

        public Result RescheduleAppointment(Guid appointmentSlotId)
        {
            if (appointmentSlotId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(appointmentSlotId));

            AppointmentSlotId = appointmentSlotId;

            return Result.Success();
        }

        public Result ChangeTestCategory(Guid testCategoryId)
        {
            if (testCategoryId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(testCategoryId));

            TestCategoryId = testCategoryId;

            return Result.Success();
        }

        public ResultT<AppointmentReminder> ScheduleReminder(
            NotificationChannel channel,
            DateTime scheduledSendTime)
        {
            if (Status is AppointmentStatus.Cancelled or AppointmentStatus.NoShow)
                return AppointmentErrors.InvalidStatus;

            var reminder = AppointmentReminder.Create(Id, channel, scheduledSendTime);

            if (reminder.IsFailure)
                return reminder.Error;

            _reminders.Add(reminder.value);

            return reminder.value;
        }
    }
}
