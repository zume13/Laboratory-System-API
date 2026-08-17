using Application.Abstractions.Base;
using Domain.Aggregates.SlotCapacity;

namespace Application.Abstractions.Repositories
{
    public interface ISlotCapacityRepository : Repository<SlotCapacityConfig>
    {
    }
}
