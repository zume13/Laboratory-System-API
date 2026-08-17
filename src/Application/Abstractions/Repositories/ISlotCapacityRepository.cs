using Application.Abstractions.Base;
using Domain.Aggregates.SlotCapacity;

namespace Application.Abstractions.Repositories
{
    public interface ISlotCapacityRepository : IRepository<SlotCapacityConfig>
    {
        Task<SlotCapacityConfig?> GetByTestCategoryIdAsync(Guid testCategoryId, CancellationToken cancellationToken = default);
    }
}
