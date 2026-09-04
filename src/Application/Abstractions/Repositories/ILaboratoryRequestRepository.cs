using Application.Abstractions.Base;
using Domain.Aggregates.LaboratoryOrder;
using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;

namespace Application.Abstractions.Repositories
{
    public interface ILaboratoryRequestRepository : IRepository<LaboratoryRequest>
    {
        Task<List<LaboratoryRequest>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
        Task<List<LaboratoryRequest>> GetUnlinkedByPhysicalPatientIdAsync(string physicalPatientId, CancellationToken cancellationToken = default);
        Task<LaboratoryRequest?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
        Task<List<LaboratoryRequest>> GetByStatusAsync(RequestStatus status, CancellationToken cancellationToken = default);
        Task<List<LaboratoryRequest>> GetPendingWithoutResultAsync(CancellationToken cancellationToken = default);
    }
}
