using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.AddLabRequest
{
    public record AddLabRequestCommand(Guid LabOrderId, Guid TestCategoryId) : IRequest<Result>;
}
