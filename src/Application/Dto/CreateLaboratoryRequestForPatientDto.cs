namespace Application.Dto
{
    public record CreateLaboratoryRequestForPatientDto(
        Guid patientId,
        Guid testCategoryId,
        string clinicalDetails,
        Guid? appointmentId);
}