using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.PatientProfile;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PatientProfileRepository : Repository<PatientProfile>, IPatientProfileRepository
    {
        public PatientProfileRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task<PatientProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.PatientProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
        }

        public async Task<PatientProfile?> GetByPhysicalPatientIdAsync(string physicalPatientId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.PatientProfiles
                .FirstOrDefaultAsync(p => p.PhysicalPatientId == physicalPatientId, cancellationToken);
        }
    }
}
