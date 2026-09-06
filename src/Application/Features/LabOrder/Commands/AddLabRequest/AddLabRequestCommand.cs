using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.AddLabRequest
{
    public record AddLabRequestCommand(Guid LabOrderId, Guid TestCategoryId) : IRequest<Result>;
}
