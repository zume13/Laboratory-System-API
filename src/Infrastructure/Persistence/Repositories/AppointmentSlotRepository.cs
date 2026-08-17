using Domain.Aggregates.AppointmentSlot;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentSlotRepository : Repository<AppointmentSlot>
    {
        public AppointmentSlotRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
