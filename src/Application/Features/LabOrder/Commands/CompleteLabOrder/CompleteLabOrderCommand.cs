using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.CompleteLabOrder
{
    public record CompleteLabOrderCommand(Guid LabOrderId) : IRequest<Result>;
}
