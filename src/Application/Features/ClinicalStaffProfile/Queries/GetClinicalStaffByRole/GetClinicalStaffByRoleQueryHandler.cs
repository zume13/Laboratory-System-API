using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Queries.GetClinicalStaffByRole
{
    public class GetClinicalStaffByRoleQueryHandler : IRequestHandler<GetClinicalStaffByRoleQuery, ResultT<List<ClinicalStaffProfileDto>>>
    {
        private readonly IClinicalProfileRepository _clinicalProfileRepository;

        public GetClinicalStaffByRoleQueryHandler(IClinicalProfileRepository clinicalProfileRepository)
        {
            _clinicalProfileRepository = clinicalProfileRepository;
        }

        public async Task<ResultT<List<ClinicalStaffProfileDto>>> Handle(GetClinicalStaffByRoleQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _clinicalProfileRepository.GetByRoleAsync(request.role, cancellationToken);
            return profiles.Select(p => new ClinicalStaffProfileDto(p.Id, p.UserId, p.Role.ToString(), p.IsActive)).ToList();
        }
    }
}