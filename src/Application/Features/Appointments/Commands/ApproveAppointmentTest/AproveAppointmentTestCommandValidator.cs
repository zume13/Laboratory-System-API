

using FluentValidation;

namespace Application.Features.Appointments.Commands.ApproveAppointmentTest
{
    public class AproveAppointmentTestCommandValidator : AbstractValidator<AproveAppointmentTestCommand>
    {
        public AproveAppointmentTestCommandValidator()
        {
            RuleFor(x => x.appointmentTestId)
                .NotEmpty()
                .WithMessage("Appointment test ID is required.");
            RuleFor(x => x.appointmentId)
                .NotEmpty()
                .WithMessage("Appointment ID is required.");
        }   
   
    }
}
