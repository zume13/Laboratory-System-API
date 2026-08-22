using Application.Abstractions.Repositories;
using Domain.Aggregates.RefreshToken;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

    public sealed class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{

        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public async Task<RefreshToken?> GetByTokenHashAsync(
            string tokenHash,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens
                    .FirstOrDefaultAsync(
                    x => x.TokenHash == tokenHash,
                    cancellationToken);
        }
  }

