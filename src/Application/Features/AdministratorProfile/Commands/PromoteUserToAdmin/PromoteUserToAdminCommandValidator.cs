using FluentValidation;

namespace Application.Features.AdministratorProfile.Commands.PromoteUserToAdmin
{
    public class PromoteUserToAdminCommandValidator : AbstractValidator<PromoteUserToAdminCommand>
    {
        public PromoteUserToAdminCommandValidator()
        {
            RuleFor(x => x.userId).NotEmpty().WithMessage("User id is required.");
        }
    }
}