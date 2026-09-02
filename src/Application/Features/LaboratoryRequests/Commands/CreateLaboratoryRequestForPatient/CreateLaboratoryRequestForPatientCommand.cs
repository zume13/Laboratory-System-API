using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Commands.CreateLaboratoryRequestForPatient
{
    public record CreateLaboratoryRequestForPatientCommand(
        Guid patientId,
        Guid testCategoryId,
        string clinicalDetails,
        Guid? appointmentId)
        : IRequest<ResultT<Guid>>;
}