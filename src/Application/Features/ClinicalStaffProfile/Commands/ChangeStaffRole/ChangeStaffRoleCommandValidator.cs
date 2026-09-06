using FluentValidation;

namespace Application.Features.ClinicalStaffProfile.Commands.ChangeStaffRole
{
    public class ChangeStaffRoleCommandValidator : AbstractValidator<ChangeStaffRoleCommand>
    {
        public ChangeStaffRoleCommandValidator()
        {
            RuleFor(s => s.staffUserId).NotEmpty().WithMessage("Staff user id is required.");
        }
    }
}