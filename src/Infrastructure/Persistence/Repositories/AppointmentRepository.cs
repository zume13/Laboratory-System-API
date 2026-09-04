using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment;
using Domain.Aggregates.Appointment.Enums;
using Domain.Aggregates.Communications.Enums;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Appointments
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Appointment?> GetByAppointmentSlotIdAsync(Guid appointmentSlotId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentSlotId == appointmentSlotId, cancellationToken);
        }

        public async Task<List<Appointment>> GetByStatusAsync(AppointmentStatus status, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Appointments
                .Where(a => a.Status == status)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetPastDueUnresolvedAsync(DateTime asOf, CancellationToken cancellationToken = default)
        {
            var unresolved = new[] { AppointmentStatus.Booked };

            return await (
                from appointment in _dbContext.Appointments
                join slot in _dbContext.AppointmentSlots on appointment.AppointmentSlotId equals slot.Id
                where unresolved.Contains(appointment.Status) && slot.Date < asOf
                select appointment)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetWithPendingRemindersDueAsync(DateTime asOf, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Appointments
                .Where(a => a.Reminders.Any(r => r.Status == NotificationStatus.Pending && r.ScheduledSendTime <= asOf))
                .ToListAsync(cancellationToken);
        }

        public async Task<Appointment?> GetAppointmentWithAppointmentTestAsync(Guid appointmentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Appointments
                .Include(a => a.Tests)
                .FirstOrDefaultAsync(
                    a => a.Id == appointmentId,
                    cancellationToken);
        }
    }
}
