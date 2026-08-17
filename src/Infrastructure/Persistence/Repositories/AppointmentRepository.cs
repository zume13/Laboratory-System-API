using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
