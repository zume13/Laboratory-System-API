using Domain.Aggregates.Identity.ClinicalStaffProfile;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;

namespace Application.Abstractions.Repositories
{
    public class ClinicalProfileRepository : Repository<ClinicalStaffProfile>
    {
        public ClinicalProfileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
