using FluentValidation;

namespace Application.Features.PatientProfile.Commands.AcceptPatientConsent
{
    public class AcceptPatientConsentCommandValidator : AbstractValidator<AcceptPatientConsentCommand>
    {
        public AcceptPatientConsentCommandValidator()
        {
            RuleFor(x => x.patientUserId).NotEmpty().WithMessage("Patient user id is required.");
        }
    }
}