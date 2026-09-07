namespace Application.Dto
{
    public record AppointmentDto(
        Guid id,
        Guid patientId,
        Guid appointmentSlotId,
        string status,
        string bookingChannel,
        DateTime createdAt,
        DateTime? confirmedAt);
}