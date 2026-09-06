using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Commands.AcceptPatientConsent
{
    public record AcceptPatientConsentCommand(Guid patientUserId) : IRequest<Result>;
}