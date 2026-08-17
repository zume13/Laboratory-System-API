using Application.Abstractions.Base;
using Domain.Aggregates.Communications.Notification;

namespace Application.Abstractions.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<Notification>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
        Task<List<Notification>> GetPendingAsync(CancellationToken cancellationToken = default);
    }
}
