namespace Application.Dto
{
    public record CreateLaboratoryRequestForWalkInDto(
        string physicalPatientId,
        Guid testCategoryId,
        string clinicalDetails);
}