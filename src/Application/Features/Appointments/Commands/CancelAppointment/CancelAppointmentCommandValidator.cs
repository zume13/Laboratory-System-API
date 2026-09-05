using FluentValidation;

namespace Application.Features.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommandValidator : AbstractValidator<CancelAppointmentCommand>    
    {
        public CancelAppointmentCommandValidator() 
        {
            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("Appointment Id is required.");
        }
    }
}
