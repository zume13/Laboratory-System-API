using Application.Abstractions.Repositories;
using Domain.Aggregates.Otp;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class OtpRepository : Repository<Otp>, IOtpRepository
    {
        public OtpRepository(ApplicationDbContext context) : base(context) { }
        public async Task<Otp?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.EmailVerificationOtps
            .Where(x =>
                x.UserId == userId &&
                x.ConsumedAt == null &&
                x.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
