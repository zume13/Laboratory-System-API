namespace Application.Dto
{
    public record AppointmentScheduleSummaryDto(
        DateTime date,
        int total,
        int booked,
        int cancelled,
        int completed,
        int noShow);
}