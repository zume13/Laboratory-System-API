using Application.Abstractions.Base;
using Domain.Aggregates.Monitoring.SystemConfig;

namespace Application.Abstractions.Repositories
{
    public interface ISystemConfigRepository : IRepository<SystemConfig>
    {
        Task<SystemConfig?> GetByKeyAsync(string key, CancellationToken cancellationToken = default);
    }
}
