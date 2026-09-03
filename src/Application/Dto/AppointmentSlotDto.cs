namespace Application.Dto
{
    public record AppointmentSlotDto(
        Guid id,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        Guid? testCategoryId,
        int capacity,
        int bookedCount,
        Guid? configuredByStaffId);
}