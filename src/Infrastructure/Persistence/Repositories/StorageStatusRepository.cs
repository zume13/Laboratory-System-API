using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.StorageStatus;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class StorageStatusRepository : Repository<StorageStatus>, IStorageStatusRepository
    {
        public StorageStatusRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task<StorageStatus?> GetByStorageTypeAsync(string storageType, CancellationToken cancellationToken = default)
        {
            return await _dbContext.StorageStatuses
                .FirstOrDefaultAsync(s => s.StorageType == storageType, cancellationToken);
        }
    }
}
