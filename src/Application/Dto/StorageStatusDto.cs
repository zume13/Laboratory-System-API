namespace Application.Dto
{
    public record StorageStatusDto(Guid id, string storageType, decimal usedGb, decimal capacityGb, decimal percentUsed, DateTime lastCheckedAt);
}