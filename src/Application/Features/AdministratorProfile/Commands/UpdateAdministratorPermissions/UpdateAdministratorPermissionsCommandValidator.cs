using FluentValidation;

namespace Application.Features.AdministratorProfile.Commands.UpdateAdministratorPermissions
{
    public class UpdateAdministratorPermissionsCommandValidator : AbstractValidator<UpdateAdministratorPermissionsCommand>
    {
        public UpdateAdministratorPermissionsCommandValidator()
        {
            RuleFor(x => x.userId).NotEmpty().WithMessage("User id is required.");
        }
    }
}