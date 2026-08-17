using Domain.Aggregates.Monitoring.SystemConfig;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;

namespace Application.Abstractions.Repositories
{
    public class SystemConfigRepository : Repository<SystemConfig>
    {
        public SystemConfigRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
