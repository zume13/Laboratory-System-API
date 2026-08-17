using Application.Abstractions.Base;
using Domain.Aggregates.Identity.PatientProfile;

namespace Application.Abstractions.Repositories
{
    public interface IPatientProfileRepository : Repository<PatientProfile>
    {
    }
}
