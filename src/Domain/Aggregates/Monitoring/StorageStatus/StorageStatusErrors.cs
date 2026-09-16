using SharedKernel.Shared;

namespace Domain.Aggregates.Monitoring.StorageStatus
{
    public static class StorageStatusErrors
    {
        public static Error NotFound(string storageType) => Error.NotFound("StorageStatus.NotFound", $"No storage status found for storage type '{storageType}'.");
        public static Error AlreadyExists(string storageType) => Error.Conflict("StorageStatus.AlreadyExists", $"A storage status for '{storageType}' already exists.");
    }
}