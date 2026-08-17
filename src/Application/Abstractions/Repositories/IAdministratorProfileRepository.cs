using Application.Abstractions.Base;
using Domain.Aggregates.Identity.AdministratorProfile;

namespace Application.Abstractions.Repositories
{
    public interface IAdministratorProfileRepository : Repository<AdministratorProfile>
    {
    }
}
