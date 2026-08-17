using Domain.Aggregates.Communications.SmsGateway;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories
{
    public class SmsGatewayRepository : Repository<SmsGatewayStatus>
    {
        public SmsGatewayRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
