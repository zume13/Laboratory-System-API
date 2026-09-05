using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.RemoveLabRequest
{
    public record RemoveLabRequestCommand(Guid LabOrderId, Guid TestCategory) : IRequest<Result>;   
}
