namespace Application.Dto
{
    public record CreateAppointmentSlotDto(DateTime date, TimeSpan startTime, TimeSpan endTime, 
        Guid? testCategoryId, int capacity);
}