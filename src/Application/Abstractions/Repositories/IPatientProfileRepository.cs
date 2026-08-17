using Application.Abstractions.Base;
using Domain.Aggregates.Identity.PatientProfile;

namespace Application.Abstractions.Repositories
{
    public interface IPatientProfileRepository : IRepository<PatientProfile>
    {
        Task<PatientProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<PatientProfile?> GetByPhysicalPatientIdAsync(string physicalPatientId, CancellationToken cancellationToken = default);
    }
}
