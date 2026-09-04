using Application.Abstractions.Base;
using Domain.Aggregates.Appointment;
using Domain.Aggregates.Appointment.Enums;

namespace Application.Abstractions.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<List<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
        Task<Appointment?> GetByAppointmentSlotIdAsync(Guid appointmentSlotId, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetByStatusAsync(AppointmentStatus status, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetPastDueUnresolvedAsync(DateTime asOf, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetWithPendingRemindersDueAsync(DateTime asOf, CancellationToken cancellationToken = default);
        Task<Appointment?> GetAppointmentWithAppointmentTestAsync(Guid appointmentId, CancellationToken cancellationToken = default);
    }
}
