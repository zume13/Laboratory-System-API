namespace Application.Dto
{
    public record UpdateAppointmentSlotDto(
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        Guid? testCategoryId,
        int capacity);
}