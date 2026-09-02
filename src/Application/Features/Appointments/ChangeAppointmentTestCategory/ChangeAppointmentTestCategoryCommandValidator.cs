using FluentValidation;

namespace Application.Features.Appointments.ChangeAppointmentTestCategory
{
    public class ChangeAppointmentTestCategoryCommandValidator : AbstractValidator<ChangeAppointmentTestCategoryCommand>
    {
        public ChangeAppointmentTestCategoryCommandValidator()
        {
            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("AppointmentId is required.");
            RuleFor(x => x.NewTestCategoryId).NotEmpty().WithMessage("NewTestCategoryId is required.");
        }
    }
}
