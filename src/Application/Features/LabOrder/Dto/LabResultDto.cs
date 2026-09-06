
namespace Application.Aggregates.Laboratory.LaboratoryOrder.Dtos
{
    public record LabResultDto
    (
        Guid Id,
        Guid LaboratoryRequestId,
        string PdfPath,
        string SampleId
    );
}
