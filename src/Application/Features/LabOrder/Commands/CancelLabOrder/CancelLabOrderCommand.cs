using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.CancelLabOrder
{
    public record CancelLabOrderCommand(Guid LabOrderId) : IRequest<Result>;
}
