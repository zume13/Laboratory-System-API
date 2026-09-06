using Domain.Aggregates.Laboratory.LaboratoryOrder.Enums;

namespace Application.Aggregates.Laboratory.LaboratoryOrder.Dtos
{
    public sealed record LabRequestDto(Guid id, string testName, RequestStatus status, DateTime createdAt, DateTime? completedAt, LabResultDto? labResult);
}
