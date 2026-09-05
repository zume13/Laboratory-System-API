using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Shared;

namespace Infrastructure.Persistence.Repositories
{
    public class LabOrderRepository : Repository<LaboratoryRequestOrder>, ILabOrderRepository
    {
        public LabOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<LaboratoryRequestOrder?> GetLabOrderWithLabRequestAsync(Guid labOrderId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabOrders.Include(x => x.Requests)
                .FirstOrDefaultAsync(x => x.Id == labOrderId, cancellationToken);
        }
    }
}
