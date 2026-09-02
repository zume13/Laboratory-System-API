using FluentValidation;

namespace Application.Features.Appointments.Commands.CancelAppointment
{
    public class CancelAppointCommandValidator : AbstractValidator<CancelAppointmentCommand>    
    {
        public CancelAppointCommandValidator() 
        {
            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("Appointment Id is required.");
        }
    }
}
