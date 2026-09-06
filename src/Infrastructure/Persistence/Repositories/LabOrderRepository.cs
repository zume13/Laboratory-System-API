using Application.Abstractions.Repositories;
using Application.Aggregates.Laboratory.LaboratoryOrder.Dtos;
using Application.Features.LabOrder.Dto;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class LabOrderRepository : Repository<LaboratoryRequestOrder>, ILabOrderRepository
    {
        public LabOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<LaboratoryRequestOrder>> GetAllLabOrdersByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabOrders
                .Include(x => x.Requests)
                .ThenInclude(x => x.labResult)
                .Where(x => x.PatientId == patientId)
                .ToListAsync(cancellationToken);
        }

        public async Task<LaboratoryRequestOrder?> GetLabOrderWithLabRequestForUpdateAsync(Guid labOrderId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabOrders
                .Include(x => x.Requests)
                .ThenInclude(x => x.labResult)
                .FirstOrDefaultAsync(x => x.Id == labOrderId, cancellationToken);
        }

        public async Task<LabOrderWithRequestDto?> GetLabOrderWithLabRequestAsync(Guid labOrderId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.LabOrders
                .AsNoTracking()
                .Where(x => x.Id == labOrderId)
                .Select(x => new LabOrderWithRequestDto(
                    x.Id,
                    x.CreatedAt,
                    x.Status,
                    x.Requests.Select(r => new LabRequestDto(
                        r.Id,
                        _dbContext.TestCategories.Where(c => c.Id == r.TestCategoryId).Select(c => c.Name.value).FirstOrDefault()!,
                        r.Status,
                        r.CreatedAt,
                        r.CompletedAt,
                        r.labResult != null ? new LabResultDto(
                            r.labResult.Id,
                            r.labResult.LaboratoryRequestId,
                            r.labResult.PdfPath.value,
                            r.labResult.SampleId
                        ) : null
                    )).ToList()
                )).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
