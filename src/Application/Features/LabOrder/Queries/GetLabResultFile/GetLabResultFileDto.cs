
namespace Application.Features.LabOrder.Queries.GetLabResultFile
{
    public record GetLabResultFileDto(
     Stream stream,
     string contentType,
     string fileName);
}
