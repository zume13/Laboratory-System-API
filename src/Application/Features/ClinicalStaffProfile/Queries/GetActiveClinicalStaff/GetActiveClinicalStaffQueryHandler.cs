using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Queries.GetActiveClinicalStaff
{
    public class GetActiveClinicalStaffQueryHandler : IRequestHandler<GetActiveClinicalStaffQuery, ResultT<List<ClinicalStaffProfileDto>>>
    {
        private readonly IClinicalProfileRepository _clinicalProfileRepository;

        public GetActiveClinicalStaffQueryHandler(IClinicalProfileRepository clinicalProfileRepository)
        {
            _clinicalProfileRepository = clinicalProfileRepository;
        }

        public async Task<ResultT<List<ClinicalStaffProfileDto>>> Handle(GetActiveClinicalStaffQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _clinicalProfileRepository.GetActiveAsync(cancellationToken);
            return profiles.Select(p => new ClinicalStaffProfileDto(p.Id, p.UserId, p.Role.ToString(), p.IsActive)).ToList();
        }
    }
}