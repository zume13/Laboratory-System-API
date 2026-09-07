using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.ReleaseLabRequest
{
    public record ReleaseLabRequestCommand(Guid LabOrderId, Guid RequestId) : IRequest<Result>;
}