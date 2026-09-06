using Application.Aggregates.Laboratory.LaboratoryOrder.Dtos;
using Domain.Aggregates.Laboratory.LaboratoryOrder.Enums;

namespace Application.Features.LabOrder.Dto
{
    public record LabOrderWithRequestDto(Guid labOrderId, DateTime CreatedAt, LabOrderStatus Status, List<LabRequestDto> Requests);
}
