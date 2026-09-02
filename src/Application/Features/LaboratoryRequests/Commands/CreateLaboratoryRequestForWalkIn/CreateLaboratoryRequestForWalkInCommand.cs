using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Commands.CreateLaboratoryRequestForWalkIn
{
    public record CreateLaboratoryRequestForWalkInCommand(
        string physicalPatientId,
        Guid testCategoryId,
        string clinicalDetails)
        : IRequest<ResultT<Guid>>;
}