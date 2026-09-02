using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Commands.AttachPatientToWalkInRequest
{
    public record AttachPatientToWalkInRequestCommand(
        Guid laboratoryRequestId,
        Guid patientId)
        : IRequest<Result>;
}