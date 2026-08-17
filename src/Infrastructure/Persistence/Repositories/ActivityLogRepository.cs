using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.ActivityLog;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories
{
    public class ActivityLogRepository : Repository<ActivityLog>, IActivityLogRepository    
    {
        public ActivityLogRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
