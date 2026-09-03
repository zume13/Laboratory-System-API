namespace Application.Dto
{
    public record SlotCapacityConfigDto(Guid id, Guid testCategoryId, int maxDailyBookings, int maxPerSlot);
}