using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.AdministratorProfile;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories
{
    public class AdministratorProfileRepository : Repository<AdministratorProfile>, IAdministratorProfileRepository
    {
        public AdministratorProfileRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
