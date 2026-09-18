using SharedKernel.Shared;

namespace Domain.Aggregates.Monitoring.SystemConfig
{
    public static class SystemConfigErrors
    {
        public static Error NotFound(string key) => Error.NotFound("SystemConfig.NotFound", $"No system config found with key '{key}'.");
        public static Error AlreadyExists(string key) => Error.Conflict("SystemConfig.AlreadyExists", $"A system config with key '{key}' already exists.");
    }
}