using Application.Abstractions.Base;
using Domain.Aggregates.Monitoring.ActivityLog;

namespace Application.Abstractions.Repositories
{
    public interface IActivityLogRepository : IRepository<ActivityLog>
    {
        Task<List<ActivityLog>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    }
}
