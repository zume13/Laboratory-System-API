namespace Application.Dto
{
    public record LaboratoryResultDto(
        Guid id,
        Guid uploadedByStaffId,
        string pdfPath,
        string sampleId,
        DateTime? releaseDate,
        bool isVoided,
        DateTime uploadedAt);
}