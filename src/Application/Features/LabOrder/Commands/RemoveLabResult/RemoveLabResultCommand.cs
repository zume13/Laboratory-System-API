using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.RemoveLabResult
{
    public record RemoveLabResultCommand(Guid labOrderId,  Guid requestId) : IRequest<Result>;
}
