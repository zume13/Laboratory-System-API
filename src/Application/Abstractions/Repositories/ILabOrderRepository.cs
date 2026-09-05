using Application.Abstractions.Base;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using SharedKernel.Shared;

namespace Application.Abstractions.Repositories
{
    public interface ILabOrderRepository : IRepository<LaboratoryRequestOrder>
    {
        Task<LaboratoryRequestOrder?> GetLabOrderWithLabRequestAsync(Guid labOrderId, CancellationToken cancellationToken = default);
    }
}
