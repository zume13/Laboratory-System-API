using FluentValidation;

namespace Application.Features.AppointmentSlots.Commands.Delete
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