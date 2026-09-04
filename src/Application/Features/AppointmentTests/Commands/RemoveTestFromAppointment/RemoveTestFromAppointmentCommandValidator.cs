using FluentValidation;

namespace Application.Features.AppointmentTests.Commands.RemoveTestFromAppointment
{
    public class RemoveTestFromAppointmentCommandValidator : AbstractValidator<RemoveTestFromAppointmentCommand>
    {
        public RemoveTestFromAppointmentCommandValidator()
        {
            RuleFor(i => i.appointmentId).NotEmpty().WithMessage("Appointment id is required.");
            RuleFor(m => m.appointmentTestId).NotEmpty().WithMessage("Appointment test id is required.");
        }
    }
}