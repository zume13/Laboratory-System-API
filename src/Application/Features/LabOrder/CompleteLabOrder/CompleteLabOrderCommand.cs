using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.CompleteLabOrder
{
    public record CompleteLabOrderCommand(Guid LabOrderId) : IRequest<Result>;
}
