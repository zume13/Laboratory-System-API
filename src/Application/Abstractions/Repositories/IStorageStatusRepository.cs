using Application.Abstractions.Base;
using Domain.Aggregates.Monitoring.StorageStatus;

namespace Application.Abstractions.Repositories
{
    public interface IStorageStatusRepository : Repository<StorageStatus>
    {
    }
}
