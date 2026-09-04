    using FluentValidation;

namespace Application.Features.Appointments.Commands.AddAppointmentTest
{
    public class AddAppointmentTestCommandValidator : AbstractValidator<AddAppointmentTestCommand>
    {
        public AddAppointmentTestCommandValidator() 
        {
            RuleFor(x => x.testCategoryId)
                .NotEmpty()
                .WithMessage("Test category ID is required.");
            RuleFor(x => x.appointmentId)
                .NotEmpty()
                .WithMessage("Appointment ID is required.");
        }
    }
}
