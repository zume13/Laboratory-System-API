using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.SlotCapacity
{
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
}
