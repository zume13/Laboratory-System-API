using FluentValidation;

namespace Application.Features.Users.LogIn
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
