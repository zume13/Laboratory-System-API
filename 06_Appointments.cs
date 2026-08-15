using SharedKernel.Primitives;
using SharedKernel.Shared;
using PDDLPortal.Domain.ValueObjects;

namespace PDDLPortal.Domain.Entities.Appointments.Slots;

public class AppointmentSlot : AggregateRoot
{
    private AppointmentSlot(
        Guid id,
        DateTime date,
        TimeRange timeRange,
        Guid? testCategoryId,
        int capacity,
        Guid? configuredByStaffId)
        : base(id)
    {
        Date = date;
        TimeRange = timeRange;
        TestCategoryId = testCategoryId;
        Capacity = capacity;
        BookedCount = 0;
        ConfiguredByStaffId = configuredByStaffId;
    }

    public DateTime Date { get; private set; }

    public TimeRange TimeRange { get; private set; }

    public Guid? TestCategoryId { get; private set; }

    public int Capacity { get; private set; }

    public int BookedCount { get; private set; }

    public Guid? ConfiguredByStaffId { get; private set; }

    public bool IsFull => BookedCount >= Capacity;

    public static ResultT<AppointmentSlot> Create(
        DateTime date,
        TimeRange timeRange,
        Guid? testCategoryId,
        int capacity,
        Guid? configuredByStaffId)
    {
        if (capacity <= 0)
            return GeneralErrors.General.Invalid(nameof(capacity));

        if (date.Date < DateTime.UtcNow.Date)
            return GeneralErrors.General.Invalid(nameof(date));

        return new AppointmentSlot(
            Guid.NewGuid(),
            date,
            timeRange,
            testCategoryId,
            capacity,
            configuredByStaffId);
    }

    // Internal — a slot's own booked count can only change through Reserve/Release
    // so the capacity invariant can never be bypassed by another aggregate.
    internal Result Reserve()
    {
        if (IsFull)
            return GeneralErrors.General.Conflict(nameof(IsFull));

        if (Date.Date < DateTime.UtcNow.Date)
            return GeneralErrors.General.Invalid(nameof(Date));

        BookedCount++;

        return Result.Success();
    }

    internal Result Release()
    {
        if (BookedCount == 0)
            return GeneralErrors.General.Conflict(nameof(BookedCount));

        BookedCount--;

        return Result.Success();
    }
}

public class SlotCapacityConfig : AggregateRoot
{
    private SlotCapacityConfig(
        Guid id,
        Guid testCategoryId,
        int maxDailyBookings,
        int maxPerSlot)
        : base(id)
    {
        TestCategoryId = testCategoryId;
        MaxDailyBookings = maxDailyBookings;
        MaxPerSlot = maxPerSlot;
    }

    public Guid TestCategoryId { get; private set; }

    public int MaxDailyBookings { get; private set; }

    public int MaxPerSlot { get; private set; }

    public static ResultT<SlotCapacityConfig> Create(
        Guid testCategoryId,
        int maxDailyBookings,
        int maxPerSlot)
    {
        if (testCategoryId == Guid.Empty)
            return GeneralErrors.General.Empty(nameof(testCategoryId));

        if (maxDailyBookings <= 0 || maxPerSlot <= 0)
            return GeneralErrors.General.Invalid(nameof(maxPerSlot));

        if (maxPerSlot > maxDailyBookings)
            return GeneralErrors.General.Invalid(nameof(maxPerSlot));

        return new SlotCapacityConfig(Guid.NewGuid(), testCategoryId, maxDailyBookings, maxPerSlot);
    }

    public Result UpdateLimits(int maxDailyBookings, int maxPerSlot)
    {
        if (maxDailyBookings <= 0 || maxPerSlot <= 0)
            return GeneralErrors.General.Invalid(nameof(maxPerSlot));

        if (maxPerSlot > maxDailyBookings)
            return GeneralErrors.General.Invalid(nameof(maxPerSlot));

        MaxDailyBookings = maxDailyBookings;
        MaxPerSlot = maxPerSlot;

        return Result.Success();
    }
}

namespace PDDLPortal.Domain.Entities.Appointments;

public enum AppointmentStatus
{
    Booked,
    Confirmed,
    Cancelled,
    Completed,
    NoShow
}

public enum BookingChannel
{
    Online,
    WalkIn
}

public class Appointment : AggregateRoot
{
    private readonly List<AppointmentReminder> _reminders = new();

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

    public Result Confirm()
    {
        if (Status != AppointmentStatus.Booked)
            return GeneralErrors.General.Conflict(nameof(Status));

        Status = AppointmentStatus.Confirmed;
        ConfirmedAt = DateTime.UtcNow;

        return Result.Success();
    }

    // Also expected to be paired with slot.Release() in the same application-layer
    // transaction, since freeing capacity is the slot's responsibility, not this one's.
    public Result Cancel()
    {
        if (Status is AppointmentStatus.Completed or AppointmentStatus.Cancelled)
            return GeneralErrors.General.Conflict(nameof(Status));

        Status = AppointmentStatus.Cancelled;

        return Result.Success();
    }

    public Result MarkNoShow()
    {
        if (Status is not (AppointmentStatus.Booked or AppointmentStatus.Confirmed))
            return GeneralErrors.General.Conflict(nameof(Status));

        Status = AppointmentStatus.NoShow;

        return Result.Success();
    }

    // Records that a LabRequest (a separate aggregate) now fulfills this appointment.
    // The LabRequest itself is created by the application layer via LabRequest.Create(...).
    public Result CompleteWithLabRequest(Guid labRequestId)
    {
        if (Status != AppointmentStatus.Confirmed)
            return GeneralErrors.General.Conflict(nameof(Status));

        if (labRequestId == Guid.Empty)
            return GeneralErrors.General.Empty(nameof(labRequestId));

        FulfillingLabRequestId = labRequestId;
        Status = AppointmentStatus.Completed;

        return Result.Success();
    }

    public ResultT<AppointmentReminder> ScheduleReminder(
        NotificationChannel channel,
        DateTime scheduledSendTime)
    {
        if (Status is AppointmentStatus.Cancelled or AppointmentStatus.NoShow)
            return GeneralErrors.General.Conflict(nameof(Status));

        var reminder = AppointmentReminder.Create(Id, channel, scheduledSendTime);

        if (reminder.IsFailure)
            return reminder.Error;

        _reminders.Add(reminder.value);

        return reminder.value;
    }
}

public class AppointmentReminder : Entity
{
    private AppointmentReminder(
        Guid id,
        Guid appointmentId,
        NotificationChannel channel,
        DateTime scheduledSendTime)
        : base(id)
    {
        AppointmentId = appointmentId;
        Channel = channel;
        ScheduledSendTime = scheduledSendTime;
        Status = NotificationStatus.Pending;
    }

    public Guid AppointmentId { get; private set; }

    public NotificationChannel Channel { get; private set; }

    public DateTime ScheduledSendTime { get; private set; }

    public NotificationStatus Status { get; private set; }

    internal static ResultT<AppointmentReminder> Create(
        Guid appointmentId,
        NotificationChannel channel,
        DateTime scheduledSendTime)
    {
        if (scheduledSendTime <= DateTime.UtcNow)
            return GeneralErrors.General.Invalid(nameof(scheduledSendTime));

        return new AppointmentReminder(Guid.NewGuid(), appointmentId, channel, scheduledSendTime);
    }

    internal void MarkSent() => Status = NotificationStatus.Sent;

    internal void MarkFailed() => Status = NotificationStatus.Failed;
}
