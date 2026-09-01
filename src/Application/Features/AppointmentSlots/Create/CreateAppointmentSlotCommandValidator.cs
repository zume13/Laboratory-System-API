using FluentValidation;

namespace Application.Features.AppointmentSlots.Create
{
    public class CreateAppointmentSlotCommandValidator : AbstractValidator<CreateAppointmentSlotCommand>
    {
        public CreateAppointmentSlotCommandValidator()
        {
            RuleFor(p => p.date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Date cannot be in the past.");

            RuleFor(r => r.endTime)
                .GreaterThan(o => o.startTime).WithMessage("End time must be after start time.");

            RuleFor(m => m.capacity)
                .GreaterThan(0).WithMessage("Capacity must be at least 1.");

            RuleFor(q => q.configuredByStaffId)
                .NotEmpty().WithMessage("Configuring staff id is required.");
        }
    }
}
