using FluentValidation;

namespace Application.Features.AppointmentSlots.Update
{
    public class UpdateAppointmentSlotCommandValidator : AbstractValidator<UpdateAppointmentSlotCommand>
    {
        public UpdateAppointmentSlotCommandValidator()
        {
            RuleFor(x => x.appointmentSlotId)
                .NotEmpty().WithMessage("Appointment slot id is required.");

            RuleFor(x => x.date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Date cannot be in the past.");

            RuleFor(x => x.endTime)
                .GreaterThan(x => x.startTime).WithMessage("End time must be after start time.");

            RuleFor(x => x.capacity)
                .GreaterThan(0).WithMessage("Capacity must be at least 1.");
        }
    }
}