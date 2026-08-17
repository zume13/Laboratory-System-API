using Domain.Aggregates.Identity.PatientProfile;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;

namespace Application.Abstractions.Repositories
{
    public class PatientProfileRepository : Repository<PatientProfile>
    {
        public PatientProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
