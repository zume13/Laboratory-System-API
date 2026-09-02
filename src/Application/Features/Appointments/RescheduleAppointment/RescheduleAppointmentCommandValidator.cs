using FluentValidation;

namespace Application.Features.Appointments.RescheduleAppointment
{
    internal class RescheduleAppointmentCommandValidator : AbstractValidator<RescheduleAppointmentCommand>
    {
        public RescheduleAppointmentCommandValidator() 
        { 
            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("AppointmentId is required.");
            RuleFor(x => x.CurrentAppointmentSlotId).NotEmpty().WithMessage("CurrentAppointmentSlotId is required.");
            RuleFor(x => x.NewAppointmentSlotId).NotEmpty().WithMessage("NewAppointmentSlotId is required.");
        }
    }
}
