using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.ActivityLog;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ActivityLogRepository : Repository<ActivityLog>, IActivityLogRepository    
    {
        public ActivityLogRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<ActivityLog>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ActivityLogs
                .Where(a => a.Timestamp >= from && a.Timestamp <= to)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync(cancellationToken);
        }

    }
}
