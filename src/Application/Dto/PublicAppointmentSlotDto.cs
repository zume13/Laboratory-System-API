namespace Application.Dto
{
    public record PublicAppointmentSlotDto(
        Guid id,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        Guid? testCategoryId,
        int spotsRemaining);
}