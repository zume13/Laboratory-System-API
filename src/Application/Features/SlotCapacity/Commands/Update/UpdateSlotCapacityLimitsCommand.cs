using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SlotCapacity.Commands.Update
{
    public record UpdateSlotCapacityLimitsCommand(
        Guid testCategoryId,
        int maxDailyBookings,
        int maxPerSlot)
        : IRequest<Result>;
}
