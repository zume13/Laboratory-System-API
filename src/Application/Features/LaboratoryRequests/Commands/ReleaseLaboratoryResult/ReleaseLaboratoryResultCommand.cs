using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Commands.ReleaseLaboratoryResult
{
    public record ReleaseLaboratoryResultCommand(Guid laboratoryRequestId) : IRequest<Result>;
}