using FluentValidation;

namespace Application.Features.LaboratoryRequests.Commands.CreateLaboratoryRequestForPatient
{
    public class CreateLaboratoryRequestForPatientCommandValidator : AbstractValidator<CreateLaboratoryRequestForPatientCommand>
    {
        public CreateLaboratoryRequestForPatientCommandValidator()
        {
            RuleFor(a => a.patientId).NotEmpty().WithMessage("Patient id required fr.");
            RuleFor(r => r.testCategoryId).NotEmpty().WithMessage("Test category needed.");
            RuleFor(a => a.clinicalDetails).MaximumLength(2000).WithMessage("Clinical details cannot exceed 2000 characters.");
        }
    }
}