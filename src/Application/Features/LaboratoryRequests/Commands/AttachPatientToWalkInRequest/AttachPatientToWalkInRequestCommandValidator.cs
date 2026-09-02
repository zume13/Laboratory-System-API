using FluentValidation;

namespace Application.Features.LaboratoryRequests.Commands.AttachPatientToWalkInRequest
{
    public class AttachPatientToWalkInRequestCommandValidator : AbstractValidator<AttachPatientToWalkInRequestCommand>
    {
        public AttachPatientToWalkInRequestCommandValidator()
        {
            RuleFor(j => j.laboratoryRequestId).NotEmpty().WithMessage("Laboratory request id is required.");
            RuleFor(r => r.patientId).NotEmpty().WithMessage("Patient id is required.");
        }
    }
}