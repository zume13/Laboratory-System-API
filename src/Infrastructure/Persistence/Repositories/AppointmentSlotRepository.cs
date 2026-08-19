using Application.Abstractions.Repositories;
using Domain.Aggregates.AppointmentSlot;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentSlotRepository : Repository<AppointmentSlot>, IAppointmentSlotRepository
    {
        public AppointmentSlotRepository(ApplicationDbContext context) : base(context) { }
        public async Task<List<AppointmentSlot>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AppointmentSlots
                .Where(s => s.Date.Date == date.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<AppointmentSlot>> GetAvailableByDateAndCategoryAsync(DateTime date, Guid testCategoryId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AppointmentSlots
                .Where(s => s.Date.Date == date.Date
                    && s.TestCategoryId == testCategoryId
                    && s.BookedCount < s.Capacity)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<AppointmentSlot>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AppointmentSlots
                .Where(s => s.Date >= from.Date && s.Date <= to.Date)
                .OrderBy(s => s.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetBookedCountForDateAsync(DateTime date, Guid testCategoryId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AppointmentSlots
                .Where(s => s.Date.Date == date.Date && s.TestCategoryId == testCategoryId)
                .SumAsync(s => s.BookedCount, cancellationToken);
        }
    }
}
