using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.ClinicalStaffProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ClinicalProfileRepository : Repository<ClinicalStaffProfile>, IClinicalProfileRepository
    {
        public ClinicalProfileRepository(ApplicationDbContext context) : base(context) { }
        public async Task<ClinicalStaffProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ClinicalStaffProfiles
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }

        public async Task<List<ClinicalStaffProfile>> GetByRoleAsync(StaffRole role, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ClinicalStaffProfiles
                .Where(h => h.Role == role)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ClinicalStaffProfile>> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.ClinicalStaffProfiles
                .Where(e => e.IsActive)
                .ToListAsync(cancellationToken);
        }
    }
}
