using Application.Abstractions.Base;
using Domain.Aggregates.AppointmentSlot;

namespace Application.Abstractions.Repositories
{
    public interface IAppointmentSlotRepository : IRepository<AppointmentSlot>
    {
        Task<List<AppointmentSlot>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<List<AppointmentSlot>> GetAvailableByDateAndCategoryAsync(DateTime date, Guid testCategoryId, CancellationToken cancellationToken = default);
        Task<List<AppointmentSlot>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
        Task<int> GetBookedCountForDateAsync(DateTime date, Guid testCategoryId, CancellationToken cancellationToken = default);
    }
}
