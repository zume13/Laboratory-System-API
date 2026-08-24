
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Monitoring.StorageStatus
{
    public class StorageStatus : AggregateRoot
    {
        private StorageStatus() { }
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

}
