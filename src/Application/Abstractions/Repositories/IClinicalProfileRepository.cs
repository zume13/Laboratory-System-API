using Application.Abstractions.Base;
using Domain.Aggregates.Identity.ClinicalStaffProfile;

namespace Application.Abstractions.Repositories
{
    public interface IClinicalProfileRepository : Repository<ClinicalStaffProfile>
    {
    }
}
