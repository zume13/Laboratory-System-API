using Application.Abstractions.Base;
using Domain.Aggregates.Monitoring.ActivityLog;
using Domain.Aggregates.Monitoring.Enums;

namespace Application.Abstractions.Repositories
{
    public interface IActivityLogRepository : IRepository<ActivityLog>
    {
        Task<List<ActivityLog>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
        Task<PagedResult<ActivityLog>> SearchAsync(
            Guid? userId,
            string? targetPrefix,
            LogSeverity? severity,
            DateTime? from,
            DateTime? to,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default
        );
    }
}
