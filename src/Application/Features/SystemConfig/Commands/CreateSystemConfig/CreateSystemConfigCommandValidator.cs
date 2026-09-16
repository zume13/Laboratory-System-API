using FluentValidation;

namespace Application.Features.SystemConfig.Commands.CreateSystemConfig
{
    public class CreateSystemConfigCommandValidator : AbstractValidator<CreateSystemConfigCommand>
    {
        public CreateSystemConfigCommandValidator()
        {
            RuleFor(x => x.key).NotEmpty().WithMessage("Key is required.");
        }
    }
}