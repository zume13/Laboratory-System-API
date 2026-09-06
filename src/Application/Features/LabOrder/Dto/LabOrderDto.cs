using Domain.Aggregates.Laboratory.LaboratoryOrder.Enums;

namespace Application.Features.LabOrder.Dto
{
    public record LabOrderDto(Guid labOrderId, DateTime CreatedAt, LabOrderStatus Status);
}
