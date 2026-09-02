namespace Application.Dto
{
    public record LaboratoryRequestDto(
        Guid id,
        Guid? patientId,
        string? physicalPatientId,
        Guid testCategoryId,
        string clinicalDetails,
        Guid? appointmentId,
        string status,
        DateTime createdAt,
        List<LaboratoryResultDto> results);
}