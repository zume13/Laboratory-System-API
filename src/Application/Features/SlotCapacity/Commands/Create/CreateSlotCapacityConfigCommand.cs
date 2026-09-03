using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SlotCapacity.Commands.Create
{
    public record CreateSlotCapacityConfigCommand(
            Guid testCategoryId,
            int maxDailyBookings,
            int maxPerSlot)
            : IRequest<ResultT<Guid>>;
}