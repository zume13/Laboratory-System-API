using Domain.Aggregates.Communications.Enums;
using Domain.Aggregates.Communications.Notification;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;
using Application.Abstractions.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<Notification>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Notifications
                .Where(n => n.PatientId == patientId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Notification>> GetPendingAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Notifications
                .Where(n => n.Status == NotificationStatus.Pending)
                .ToListAsync(cancellationToken);
        }

    }
}