using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace PDDLPortal.Domain.Entities.Notifications;

public enum NotificationChannel
{
    InPortal,
    SMS,
    Email
}

public enum NotificationStatus
{
    Pending,
    Sent,
    Failed
}

public class Notification : AggregateRoot
{
    private Notification(
        Guid id,
        Guid patientId,
        NotificationChannel channel,
        string message,
        Guid? labResultId)
        : base(id)
    {
        PatientId = patientId;
        Channel = channel;
        Message = message;
        LabResultId = labResultId;
        Status = NotificationStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid PatientId { get; private set; }

    public Guid? LabResultId { get; private set; }

    public NotificationChannel Channel { get; private set; }

    public NotificationStatus Status { get; private set; }

    public string Message { get; private set; }

    public DateTime? SentAt { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public static ResultT<Notification> Dispatch(
        Guid patientId,
        NotificationChannel channel,
        string message,
        Guid? labResultId = null)
    {
        if (patientId == Guid.Empty)
            return GeneralErrors.General.Empty(nameof(patientId));

        if (string.IsNullOrWhiteSpace(message))
            return GeneralErrors.General.Empty(nameof(message));

        return new Notification(Guid.NewGuid(), patientId, channel, message, labResultId);
    }

    public Result MarkSent()
    {
        if (Status != NotificationStatus.Pending)
            return GeneralErrors.General.Conflict(nameof(Status));

        Status = NotificationStatus.Sent;
        SentAt = DateTime.UtcNow;

        return Result.Success();
    }

    public Result MarkFailed()
    {
        if (Status != NotificationStatus.Pending)
            return GeneralErrors.General.Conflict(nameof(Status));

        Status = NotificationStatus.Failed;

        return Result.Success();
    }
}

namespace PDDLPortal.Domain.Entities.Notifications.Sms;

public class SmsGatewayStatus : AggregateRoot
{
    private SmsGatewayStatus(
        Guid id,
        string status,
        int creditsRemaining)
        : base(id)
    {
        Status = status;
        CreditsRemaining = creditsRemaining;
        LastUpdated = DateTime.UtcNow;
    }

    public string Status { get; private set; }

    public int CreditsRemaining { get; private set; }

    public int ErrorCount24h { get; private set; }

    public DateTime LastUpdated { get; private set; }

    public static ResultT<SmsGatewayStatus> Initialize(string status, int creditsRemaining)
    {
        if (string.IsNullOrWhiteSpace(status))
            return GeneralErrors.General.Empty(nameof(status));

        if (creditsRemaining < 0)
            return GeneralErrors.General.Invalid(nameof(creditsRemaining));

        return new SmsGatewayStatus(Guid.NewGuid(), status, creditsRemaining);
    }

    public Result RecordDeliveryFailure()
    {
        ErrorCount24h++;
        LastUpdated = DateTime.UtcNow;

        return Result.Success();
    }

    public Result ConsumeCredits(int amount)
    {
        if (amount <= 0)
            return GeneralErrors.General.Invalid(nameof(amount));

        if (amount > CreditsRemaining)
            return GeneralErrors.General.Conflict(nameof(CreditsRemaining));

        CreditsRemaining -= amount;
        LastUpdated = DateTime.UtcNow;

        return Result.Success();
    }

    public Result TopUpCredits(int amount)
    {
        if (amount <= 0)
            return GeneralErrors.General.Invalid(nameof(amount));

        CreditsRemaining += amount;
        LastUpdated = DateTime.UtcNow;

        return Result.Success();
    }
}
