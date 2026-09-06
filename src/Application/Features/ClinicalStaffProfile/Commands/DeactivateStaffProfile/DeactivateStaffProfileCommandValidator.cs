using FluentValidation;

namespace Application.Features.ClinicalStaffProfile.Commands.DeactivateStaffProfile
{
    public class DeactivateStaffProfileCommandValidator : AbstractValidator<DeactivateStaffProfileCommand>
    {
        public DeactivateStaffProfileCommandValidator()
        {
            RuleFor(x => x.staffUserId).NotEmpty().WithMessage("Staff user id is required.");
        }
    }
}