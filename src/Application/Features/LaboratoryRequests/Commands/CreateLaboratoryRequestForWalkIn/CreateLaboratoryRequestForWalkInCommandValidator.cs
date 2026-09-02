using FluentValidation;

namespace Application.Features.LaboratoryRequests.Commands.CreateLaboratoryRequestForWalkIn
{
    public class CreateLaboratoryRequestForWalkInCommandValidator : AbstractValidator<CreateLaboratoryRequestForWalkInCommand>
    {
        public CreateLaboratoryRequestForWalkInCommandValidator()
        {
            RuleFor(x => x.physicalPatientId).NotEmpty().WithMessage("Patient id required fr.");
            RuleFor(c => c.testCategoryId).NotEmpty().WithMessage("Test category ID is required.");
            RuleFor(x => x.clinicalDetails).MaximumLength(2000).WithMessage("Clinical details cannot exceed 2000 characters.");
        }
    }
}