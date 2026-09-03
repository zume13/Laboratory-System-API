using FluentValidation;

namespace Application.Features.SlotCapacity.Commands.Create
{
    public class CreateSlotCapacityConfigCommandValidator : AbstractValidator<CreateSlotCapacityConfigCommand>
    {
        public CreateSlotCapacityConfigCommandValidator()
        {
            RuleFor(x => x.testCategoryId).NotEmpty().WithMessage("Test category id is required.");
            RuleFor(x => x.maxDailyBookings).GreaterThan(0).WithMessage("Max daily bookings must be at least 1.");
            RuleFor(x => x.maxPerSlot).GreaterThan(0).WithMessage("Max per slot must be at least 1.");
        }
    }
}
