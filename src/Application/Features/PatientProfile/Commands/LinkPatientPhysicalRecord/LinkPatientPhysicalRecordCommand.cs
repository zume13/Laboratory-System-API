using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Commands.LinkPatientPhysicalRecord
{
    public record LinkPatientPhysicalRecordCommand(Guid patientUserId, string physicalPatientId) : IRequest<Result>;
}