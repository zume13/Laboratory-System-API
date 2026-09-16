using FluentValidation;

namespace Application.Features.SystemConfig.Commands.UpdateSystemConfigValue
{
    public class UpdateSystemConfigValueCommandValidator : AbstractValidator<UpdateSystemConfigValueCommand>
    {
        public UpdateSystemConfigValueCommandValidator()
        {
            RuleFor(x => x.key).NotEmpty().WithMessage("Key required.");
        }
    }
}