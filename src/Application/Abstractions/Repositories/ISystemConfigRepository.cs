using Application.Abstractions.Base;
using Domain.Aggregates.Monitoring.SystemConfig;

namespace Application.Abstractions.Repositories
{
    public interface ISystemConfigRepository : Repository<SystemConfig>
    {
    }
}
