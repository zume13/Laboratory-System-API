using Application.Abstractions.Base;
using Domain.Aggregates.Monitoring.StorageStatus;

namespace Application.Abstractions.Repositories
{
    public interface IStorageStatusRepository : IRepository<StorageStatus>
    {
        Task<StorageStatus?> GetByStorageTypeAsync(string storageType, CancellationToken cancellationToken = default);
    }
}
