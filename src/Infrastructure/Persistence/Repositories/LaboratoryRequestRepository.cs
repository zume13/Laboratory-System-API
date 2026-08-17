using Domain.Aggregates.Laboratory.LaboratoryRequest;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;

namespace Application.Abstractions.Repositories
{
    public class LaboratoryRequestRepository : Repository<LaboratoryRequest>
    {
        public LaboratoryRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
