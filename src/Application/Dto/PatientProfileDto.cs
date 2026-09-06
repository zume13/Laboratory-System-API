namespace Application.Dto
{
    public record PatientProfileDto(
        Guid id,
        Guid userId,
        DateOnly dateOfBirth,
        string sex,
        string? physicalPatientId,
        bool consentAccepted);
}