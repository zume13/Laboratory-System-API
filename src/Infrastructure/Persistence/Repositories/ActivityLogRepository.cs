using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.ActivityLog;
using Domain.Aggregates.Monitoring.Enums;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Application.Abstractions.Base;

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

        public async Task<PagedResult<ActivityLog>> SearchAsync(
        Guid? userId, string? targetPrefix,LogSeverity? severity,DateTime? from,DateTime? to,int page,int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.ActivityLogs.AsNoTracking();

            if (userId.HasValue)
                query = query.Where(a => a.UserId == userId.Value);

            if (!string.IsNullOrWhiteSpace(targetPrefix))
                query = query.Where(a => a.Target.StartsWith(targetPrefix));

            if (severity.HasValue)
                query = query.Where(a => a.Severity == severity.Value);

            if (from.HasValue)
                query = query.Where(a => a.Timestamp >= from.Value);

            if (to.HasValue)
                query = query.Where(a => a.Timestamp <= to.Value);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(a => a.Timestamp)
                .ThenByDescending(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ActivityLog>(items, totalCount, page, pageSize);
        }

    }
}
