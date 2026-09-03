using FluentValidation;

namespace Application.Features.SlotCapacity.Commands.Update
{
    public class UpdateSlotCapacityLimitsCommandValidator : AbstractValidator<UpdateSlotCapacityLimitsCommand>
    {
        public UpdateSlotCapacityLimitsCommandValidator()
        {
            RuleFor(x => x.testCategoryId).NotEmpty().WithMessage("Test category id is required.");
            RuleFor(x => x.maxDailyBookings).GreaterThan(0).WithMessage("Max daily bookings must be at least 1.");
            RuleFor(x => x.maxPerSlot).GreaterThan(0).WithMessage("Max per slot must be at least 1.");
        }
    }
}