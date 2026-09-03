using Domain.Aggregates.AppointmentSlot;
using Domain.Aggregates.SlotCapacity;
using SharedKernel.Shared;

namespace Domain.Services
{
    public static class SlotCapacityValidationService
    {
        public static Result ValidateNewSlotCapacity(
            SlotCapacityConfig config,
            IEnumerable<AppointmentSlot> existingSlotsOnDate,
            int requestedCapacity,
            Guid? excludingSlotId = null)
        {
            if (requestedCapacity > config.MaxPerSlot)
                return SlotCapacityErrors.ExceedsMaxPerSlot(requestedCapacity, config.MaxPerSlot);

            var alreadyAllocated = existingSlotsOnDate
                .Where(s => s.TestCategoryId == config.TestCategoryId && s.Id != excludingSlotId)
                .Sum(s => s.Capacity);

            if (alreadyAllocated + requestedCapacity > config.MaxDailyBookings)
                return SlotCapacityErrors.ExceedsMaxDailyBookings(alreadyAllocated + requestedCapacity, config.MaxDailyBookings);

            return Result.Success();
        }
    }
}