using Domain.Aggregates.Appointment.Enums;
using Domain.Aggregates.Communications.Enums;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Appointment
{
    public class Appointment : AggregateRoot
    {
        private readonly List<AppointmentTest> _tests = new();
        private readonly List<AppointmentReminder> _reminders = new();

        private Appointment() { }
        private Appointment(
            Guid id,
            Guid patientId,
            Guid appointmentSlotId,
            BookingChannel bookingChannel)
            : base(id)
        {
            PatientId = patientId;
            AppointmentSlotId = appointmentSlotId;
            BookingChannel = bookingChannel;
            Status = AppointmentStatus.Booked;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid PatientId { get; private set; }

        public Guid AppointmentSlotId { get; private set; }

        public AppointmentStatus Status { get; private set; }

        public BookingChannel BookingChannel { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? ConfirmedAt { get; private set; }

        public IReadOnlyCollection<AppointmentTest> Tests => _tests.AsReadOnly();

        public IReadOnlyCollection<AppointmentReminder> Reminders => _reminders.AsReadOnly();

        // NOTE: Booking touches two aggregates (Appointment + AppointmentSlot), so this
        // factory is meant to be called from a domain service that loads the slot,
        // calls slot.Reserve() first, and only creates the Appointment if that succeeds —
        // e.g. AppointmentBookingService in the application layer.
        public static ResultT<Appointment> Create(
            Guid patientId,
            Guid appointmentSlotId,
            BookingChannel bookingChannel,
            IEnumerable<Guid> testCategoryIds)
        {
            if (patientId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(patientId));

            if (appointmentSlotId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(appointmentSlotId));

            var distinctTestCategoryIds = (testCategoryIds ?? Enumerable.Empty<Guid>())
                .Distinct()
                .ToList();

            if (distinctTestCategoryIds.Count == 0)
                return AppointmentErrors.NoTestsProvided;

            if (distinctTestCategoryIds.Any(id => id == Guid.Empty))
                return GeneralErrors.General.Empty(nameof(testCategoryIds));

            var appointment = new Appointment(
                Guid.NewGuid(),
                patientId,
                appointmentSlotId,
                bookingChannel);

            foreach (var testCategoryId in distinctTestCategoryIds)
            {
                var test = AppointmentTest.Create(appointment.Id, testCategoryId);

                if (test.IsFailure)
                    return test.Error;

                appointment._tests.Add(test.value);
            }

            return appointment;
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
            if (Status is not AppointmentStatus.Booked)
                return AppointmentErrors.InvalidStatus;

            Status = AppointmentStatus.NoShow;

            return Result.Success();
        }

        // Adds another test to a still-booked appointment (e.g. the patient requests
        // an extra panel at check-in). Each test is fulfilled by its own LabRequest
        // downstream, so they can complete independently of one another.
        public ResultT<AppointmentTest> AddTest(Guid testCategoryId)
        {
            if (Status != AppointmentStatus.Booked)
                return AppointmentErrors.InvalidStatus;

            var test = AppointmentTest.Create(Id, testCategoryId);

            if (test.IsFailure)
                return test.Error;

            _tests.Add(test.value);

            return test.value;
        }

        // Cancels a single test before it's been fulfilled — e.g. the patient decides
        // against one of several panels. Won't remove the last remaining pending test;
        // cancel the whole appointment instead if none should go ahead.
        public Result RemoveTest(Guid appointmentTestId)
        {
            if (Status != AppointmentStatus.Booked)
                return AppointmentErrors.InvalidStatus;

            var test = _tests.FirstOrDefault(t => t.Id == appointmentTestId);

            if (test is null)
                return AppointmentErrors.TestNotFound;

            return Result.Success();
        }

        public Result RescheduleAppointment(Guid appointmentSlotId)
        {
            if (appointmentSlotId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(appointmentSlotId));

            AppointmentSlotId = appointmentSlotId;

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

        public Result Complete()
        {
            if (Status != AppointmentStatus.Booked)
                return AppointmentErrors.InvalidStatus;

            Status = AppointmentStatus.Completed;

            return Result.Success();
        }

        public Result ApproveAppointmentTest(Guid appointmentTestId)
        {
            if (Status != AppointmentStatus.Booked)
                return AppointmentErrors.InvalidStatus;

            var test = _tests.FirstOrDefault(t => t.Id == appointmentTestId);

            if (test is null)
                return AppointmentErrors.TestNotFound;

            var approveResult = test.Approve();

            if (approveResult.IsFailure)
                return approveResult.Error;

            return Result.Success();
        }

        public Result RejectAppointmentTest(Guid appointmentTestId)
        {
            if (Status != AppointmentStatus.Booked)
                return AppointmentErrors.InvalidStatus;

            var test = _tests.FirstOrDefault(t => t.Id == appointmentTestId);

            if (test is null)
                return AppointmentErrors.TestNotFound;

            var rejectResult = test.Cancel();

            if (rejectResult.IsFailure)
                return rejectResult.Error;

            return Result.Success();
        }
    }
}