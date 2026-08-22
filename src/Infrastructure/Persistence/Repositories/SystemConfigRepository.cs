using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.SystemConfig;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SystemConfigRepository : Repository<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<SystemConfig?> GetByKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            return await _dbContext.SystemConfigs
                .FirstOrDefaultAsync(s => s.Key == key, cancellationToken);
        }
    }
}
