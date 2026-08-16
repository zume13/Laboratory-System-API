using Domain.Aggregates.Monitoring.Enums;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Monitoring.ActivityLog
{
    public class ActivityLog : AggregateRoot
    {
        private ActivityLog(
            Guid id,
            Guid? userId,
            string action,
            string target,
            LogSeverity severity)
            : base(id)
        {
            UserId = userId;
            Action = action;
            Target = target;
            Severity = severity;
            Timestamp = DateTime.UtcNow;
        }

        public Guid? UserId { get; private set; }

        public string Action { get; private set; }

        public string Target { get; private set; }

        public LogSeverity Severity { get; private set; }

        public DateTime Timestamp { get; private set; }

        public static ResultT<ActivityLog> Record(
            Guid? userId,
            string action,
            string target,
            LogSeverity severity = LogSeverity.Info)
        {
            if (string.IsNullOrWhiteSpace(action))
                return GeneralErrors.General.Empty(nameof(action));

            return new ActivityLog(Guid.NewGuid(), userId, action, target ?? string.Empty, severity);
        }
    }
}
