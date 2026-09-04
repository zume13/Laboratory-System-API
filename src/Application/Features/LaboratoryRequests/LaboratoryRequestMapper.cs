using Application.Dto;
using Domain.Aggregates.LaboratoryOrder;
using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;

namespace Application.Features.LaboratoryRequests
{
    public static class LaboratoryRequestMapper
    {
        // helper to remove redundancy
        public static LaboratoryRequestDto ToDto(LaboratoryRequest request)
        {
            var resultDtos = request.Results
                .Select(c => new LaboratoryResultDto(
                    c.Id, c.UploadedByStaffId, c.PdfPath.value, c.SampleId, c.ReleaseDate, c.IsVoided, c.UploadedAt))
                .ToList();

            return new LaboratoryRequestDto(
                request.Id,
                request.PatientId,
                request.PhysicalPatientId,
                request.TestCategoryId,
                request.ClinicalDetails,
                request.AppointmentId,
                request.Status.ToString(),
                request.CreatedAt,
                resultDtos);
        }
    }
}