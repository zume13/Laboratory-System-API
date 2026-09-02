using FluentValidation;

namespace Application.Features.Appointments.CancelAppointment
{
    public class CancelAppointCommandValidator : AbstractValidator<CancelAppointmentCommand>    
    {
        public CancelAppointCommandValidator() 
        {
            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("Appointment Id is required.");
        }
    }
}
