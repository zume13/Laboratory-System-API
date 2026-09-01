using FluentValidation;

namespace Application.Features.AppointmentSlots.Delete
{
    public class DeleteAppointmentSlotCommandValidator : AbstractValidator<DeleteAppointmentSlotCommand>
    {
        public DeleteAppointmentSlotCommandValidator()
        {
            RuleFor(x => x.appointmentSlotId)
                .NotEmpty().WithMessage("Appointment slot id is required.");
        }
    }
}