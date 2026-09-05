using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.CancelLabOrder
{
    public record CancelLabOrderCommand(Guid LabOrderId) : IRequest<Result>;
}
