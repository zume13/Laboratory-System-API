using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.CompleteLabRequest
{
    public record CompleteLabRequestCommand(Guid LabOrderId, Guid RequestId) : IRequest<Result>;
}