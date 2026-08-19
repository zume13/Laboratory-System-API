using Application.Abstractions.Repositories;
using Domain.Aggregates.Communications.SmsGateway;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SmsGatewayRepository : Repository<SmsGatewayStatus>, ISmsGatewayRepository
    {
        public SmsGatewayRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<SmsGatewayStatus?> GetCurrentAsync(CancellationToken cancellationToken = default) // no dedicated current one in aggregate, so i assumed lastUpdated to be it, thx
        {
            return await _dbContext.SmsGatewayStatuses
                .OrderByDescending(s => s.LastUpdated)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
