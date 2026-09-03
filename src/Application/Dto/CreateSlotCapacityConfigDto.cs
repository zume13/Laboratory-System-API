namespace Application.Dto
{
    public record CreateSlotCapacityConfigDto(Guid testCategoryId, int maxDailyBookings, int maxPerSlot);
}