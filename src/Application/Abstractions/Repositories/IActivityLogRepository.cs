using Application.Abstractions.Base;
using Domain.Aggregates.Monitoring.ActivityLog;

namespace Application.Abstractions.Repositories
{
    public interface IActivityLogRepository : Repository<ActivityLog>
    {
    }
}
