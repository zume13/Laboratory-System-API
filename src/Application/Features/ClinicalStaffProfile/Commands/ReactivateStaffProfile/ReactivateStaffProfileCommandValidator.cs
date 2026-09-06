using FluentValidation;

namespace Application.Features.ClinicalStaffProfile.Commands.ReactivateStaffProfile
{
    public class ReactivateStaffProfileCommandValidator : AbstractValidator<ReactivateStaffProfileCommand>
    {
        public ReactivateStaffProfileCommandValidator()
        {
            RuleFor(x => x.staffUserId).NotEmpty().WithMessage("Staff user id is required.");
        }
    }
}