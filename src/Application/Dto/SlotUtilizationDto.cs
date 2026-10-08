namespace Application.Dto
{
    public record SlotUtilizationDto(
        Guid testCategoryId,
        string testCategoryName,
        int totalCapacity,
        int totalBooked,
        int? maxDailyBookings,
        decimal? percentOfDailyMaxUsed);
}