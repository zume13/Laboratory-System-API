using FluentValidation;

namespace Application.Features.PatientProfile.Commands.LinkPatientPhysicalRecord
{
    public class LinkPatientPhysicalRecordCommandValidator : AbstractValidator<LinkPatientPhysicalRecordCommand>
    {
        public LinkPatientPhysicalRecordCommandValidator()
        {
            RuleFor(x => x.patientUserId).NotEmpty().WithMessage("Patient user id is required.");
            RuleFor(x => x.physicalPatientId).NotEmpty().WithMessage("Physical patient id is required.");
        }
    }
}