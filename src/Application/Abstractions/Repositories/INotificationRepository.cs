using Application.Abstractions.Base;
using Domain.Aggregates.Communications.Notification;

namespace Application.Abstractions.Repositories
{
    public interface INotificationRepository : Repository<Notification>
    {
    }
}
