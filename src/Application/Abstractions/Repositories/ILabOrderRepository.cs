using Application.Abstractions.Base;
using Application.Features.LabOrder.Dto;
using Domain.Aggregates.Laboratory.LaboratoryOrder;

namespace Application.Abstractions.Repositories
{
    public interface ILabOrderRepository : IRepository<LaboratoryRequestOrder>
    {
        Task<List<LaboratoryRequestOrder>> GetAllLabOrdersByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
        Task<LaboratoryRequestOrder?> GetLabOrderWithLabRequestForUpdateAsync(Guid labOrderId, CancellationToken cancellationToken = default);
        Task<LabOrderWithRequestDto?> GetLabOrderWithLabRequestAsync(Guid labOrderId, CancellationToken cancellationToken = default);
    }
}
