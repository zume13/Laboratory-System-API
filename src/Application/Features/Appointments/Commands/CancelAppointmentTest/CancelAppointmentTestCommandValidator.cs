using FluentValidation;

namespace Application.Features.Appointments.Commands.CancelAppointmentTest
{
    public class CancelAppointmentTestCommandValidator : AbstractValidator<CancelAppointmentTestCommand>
    {
        public CancelAppointmentTestCommandValidator()
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