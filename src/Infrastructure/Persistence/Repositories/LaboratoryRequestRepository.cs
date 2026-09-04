using Application.Abstractions.Repositories;
using Domain.Aggregates.LaboratoryOrder;
using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class LaboratoryRequestRepository : Repository<LaboratoryRequest>, ILaboratoryRequestRepository
    {
        public LaboratoryRequestRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        
        public async Task<List<LaboratoryRequest>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabRequests
                .Where(l => l.PatientId == patientId)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<LaboratoryRequest>> GetUnlinkedByPhysicalPatientIdAsync(string physicalPatientId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabRequests
                .Where(l => l.PhysicalPatientId == physicalPatientId && l.PatientId == null)
                .ToListAsync(cancellationToken);
        }

        public async Task<LaboratoryRequest?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabRequests
                .FirstOrDefaultAsync(l => l.AppointmentId == appointmentId, cancellationToken);
        }

        public async Task<List<LaboratoryRequest>> GetByStatusAsync(RequestStatus status, CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabRequests
                .Where(l => l.Status == status)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<LaboratoryRequest>> GetPendingWithoutResultAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabRequests
                .Where(l => l.Status == RequestStatus.Pending && !l.Results.Any(r => !r.IsVoided))
                .ToListAsync(cancellationToken);
        }
    }
}
