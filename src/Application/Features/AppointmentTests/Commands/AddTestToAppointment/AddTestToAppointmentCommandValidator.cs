using FluentValidation;

namespace Application.Features.AppointmentTests.Commands.AddTestToAppointment
{
    public class AddTestToAppointmentCommandValidator : AbstractValidator<AddTestToAppointmentCommand>
    {
        public AddTestToAppointmentCommandValidator()
        {
            RuleFor(j => j.appointmentId).NotEmpty().WithMessage("Appointment id is required.");
            RuleFor(a => a.testCategoryId).NotEmpty().WithMessage("Test category id is required.");
        }
    }
}