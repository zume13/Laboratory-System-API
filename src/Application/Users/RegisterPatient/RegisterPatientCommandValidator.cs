using FluentValidation;

namespace Application.Users.RegisterPatient
{
    public class RegisterPatientCommandValidator : AbstractValidator<RegisterPatientCommand>
    {
        public RegisterPatientCommandValidator()
        {
            RuleFor(x => x.firstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.lastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.email).NotEmpty().EmailAddress().WithMessage("A valid email address is required.");
            RuleFor(x => x.phoneNumber).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
        }
    }
}
