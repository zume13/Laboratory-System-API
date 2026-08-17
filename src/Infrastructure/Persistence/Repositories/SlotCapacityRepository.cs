using Domain.Aggregates.SlotCapacity;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;

namespace Application.Abstractions.Repositories
{
    public class SlotCapacityRepository : Repository<SlotCapacityConfig>
    {
        public SlotCapacityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
