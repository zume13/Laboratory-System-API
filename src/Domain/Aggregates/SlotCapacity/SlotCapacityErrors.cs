using SharedKernel.Shared;

namespace Domain.Aggregates.SlotCapacity
{
    public static class SlotCapacityErrors
    {
        public static Error NotFound(Guid testCategoryId) => Error.NotFound("SlotCapacityConfig.NotFound", $"No slot capacity configuration found for test category '{testCategoryId}'.");
        public static Error AlreadyExists(Guid testCategoryId) => Error.Conflict("SlotCapacityConfig.AlreadyExists", $"A slot capacity configuration already exists for test category '{testCategoryId}'.");
        public static Error ExceedsMaxPerSlot(int requested, int max) => Error.Conflict("SlotCapacityConfig.ExceedsMaxPerSlot", $"Requested capacity {requested} exceeds the maximum of {max} allowed per slot for this category.");
        public static Error ExceedsMaxDailyBookings(int totalRequested, int max) => Error.Conflict("SlotCapacityConfig.ExceedsMaxDailyBookings", $"This would bring the day's total to {totalRequested}, exceeding the daily maximum of {max} for this category.");
    }
}