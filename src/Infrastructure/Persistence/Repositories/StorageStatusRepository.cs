using Domain.Aggregates.Monitoring.StorageStatus;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;

namespace Application.Abstractions.Repositories
{
    public class StorageStatusRepository : Repository<StorageStatus>
    {
        public StorageStatusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
