

using SharedKernel.Shared;

namespace Domain.Aggregates.Communications.Notification
{
    public static class NotificationErrors
    {
        public static readonly Error InvalidNotificationChannel = Error.Conflict("Notification.InvalidChannel", "Invalid notification channel.");
        public static readonly Error InvalidNotificationStatus = Error.Conflict("Notification.InvalidStatus", "Invalid notification status.");
    }
}
