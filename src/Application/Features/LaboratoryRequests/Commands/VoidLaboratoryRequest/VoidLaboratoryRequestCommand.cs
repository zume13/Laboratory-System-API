using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Commands.VoidLaboratoryRequest
{
    public record VoidLaboratoryRequestCommand(Guid laboratoryRequestId) : IRequest<Result>;
}