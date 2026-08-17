using Application.Abstractions.Base;
using Domain.Aggregates.Appointment;

namespace Application.Abstractions.Repositories
{
    public interface IAppointmentRepository : Repository<Appointment>
    {
    }
}
