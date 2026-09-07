namespace Application.Dto
{
    public record AppointmentWithTestsDto(
        Guid id,
        Guid patientId,
        Guid appointmentSlotId,
        string status,
        List<AppointmentTestDto> tests);
}