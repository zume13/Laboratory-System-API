using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace PDDLPortal.Domain.Entities.Monitoring;

public enum LogSeverity
{
    Info,
    Warning,
    High
}

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

    // Append-only aggregate — no mutation methods beyond creation
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

public class StorageStatus : AggregateRoot
{
    private StorageStatus(
        Guid id,
        string storageType,
        decimal capacityGb)
        : base(id)
    {
        StorageType = storageType;
        CapacityGb = capacityGb;
        LastCheckedAt = DateTime.UtcNow;
    }

    public string StorageType { get; private set; }

    public decimal UsedGb { get; private set; }

    public decimal CapacityGb { get; private set; }

    public DateTime LastCheckedAt { get; private set; }

    public decimal PercentUsed => CapacityGb == 0 ? 0 : Math.Round(UsedGb / CapacityGb * 100, 1);

    public static ResultT<StorageStatus> Initialize(string storageType, decimal capacityGb)
    {
        if (string.IsNullOrWhiteSpace(storageType))
            return GeneralErrors.General.Empty(nameof(storageType));

        if (capacityGb <= 0)
            return GeneralErrors.General.Invalid(nameof(capacityGb));

        return new StorageStatus(Guid.NewGuid(), storageType, capacityGb);
    }

    public Result UpdateUsage(decimal usedGb)
    {
        if (usedGb < 0 || usedGb > CapacityGb)
            return GeneralErrors.General.Invalid(nameof(usedGb));

        UsedGb = usedGb;
        LastCheckedAt = DateTime.UtcNow;

        return Result.Success();
    }
}

public class SystemConfig : AggregateRoot
{
    private SystemConfig(
        Guid id,
        string key,
        string value)
        : base(id)
    {
        Key = key;
        Value = value;
        UpdatedAt = DateTime.UtcNow;
    }

    public string Key { get; private set; }

    public string Value { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static ResultT<SystemConfig> Set(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            return GeneralErrors.General.Empty(nameof(key));

        return new SystemConfig(Guid.NewGuid(), key, value ?? string.Empty);
    }

    public Result UpdateValue(string value)
    {
        Value = value ?? string.Empty;
        UpdatedAt = DateTime.UtcNow;

        return Result.Success();
    }
}
