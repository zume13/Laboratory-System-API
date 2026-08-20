using Application.Abstractions.Repositories;
using Domain.Aggregates.SlotCapacity;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SlotCapacityRepository : Repository<SlotCapacityConfig>, ISlotCapacityRepository
    {
        public SlotCapacityRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task<SlotCapacityConfig?> GetByTestCategoryIdAsync(Guid testCategoryId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.SlotCapacityConfigs
                .FirstOrDefaultAsync(s => s.TestCategoryId == testCategoryId, cancellationToken);
        }
    }
}