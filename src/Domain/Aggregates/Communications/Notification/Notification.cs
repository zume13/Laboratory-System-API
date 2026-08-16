using Domain.Aggregates.Communications.Enums;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Communications.Notification
{
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

            if (!Enum.IsDefined(typeof(NotificationChannel), channel))
                return NotificationErrors.InvalidNotificationChannel;

            return new Notification(Guid.NewGuid(), patientId, channel, message, labResultId);
        }

        public Result MarkSent()
        {
            if (Status != NotificationStatus.Pending)
                return NotificationErrors.InvalidNotificationStatus;

            Status = NotificationStatus.Sent;
            SentAt = DateTime.UtcNow;

            return Result.Success();
        }

        public Result MarkFailed()
        {
            if (Status != NotificationStatus.Pending)
                return NotificationErrors.InvalidNotificationStatus;

            Status = NotificationStatus.Failed;

            return Result.Success();
        }
    }
}
