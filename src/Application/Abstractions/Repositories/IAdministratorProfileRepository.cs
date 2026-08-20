using Application.Abstractions.Base;
using Domain.Aggregates.Identity.AdministratorProfile;

namespace Application.Abstractions.Repositories.Identity
{
    public interface IAdministratorProfileRepository : IRepository<AdministratorProfile>
    {
        Task<AdministratorProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
