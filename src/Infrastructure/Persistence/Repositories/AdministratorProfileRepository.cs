using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.AdministratorProfile;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AdministratorProfileRepository : Repository<AdministratorProfile>, IAdministratorProfileRepository
    {
        public AdministratorProfileRepository(ApplicationDbContext context) : base(context) { }
        public async Task<AdministratorProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AdministratorProfiles
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }
    }
}