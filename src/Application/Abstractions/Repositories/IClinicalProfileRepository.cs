using Application.Abstractions.Base;
using Domain.Aggregates.Identity.ClinicalStaffProfile;
using Domain.Aggregates.Identity.User.Enums;

namespace Application.Abstractions.Repositories
{
    public interface IClinicalProfileRepository : IRepository<ClinicalStaffProfile>
    {
        Task<ClinicalStaffProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<ClinicalStaffProfile>> GetByRoleAsync(StaffRole role, CancellationToken cancellationToken = default);
        Task<List<ClinicalStaffProfile>> GetActiveAsync(CancellationToken cancellationToken = default);
    }
}
