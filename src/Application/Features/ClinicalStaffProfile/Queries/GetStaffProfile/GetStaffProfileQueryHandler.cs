using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Identity.ClinicalStaffProfile;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Queries.GetStaffProfile
{
    public class GetMyStaffProfileQueryHandler : IRequestHandler<GetStaffProfileQuery, ResultT<ClinicalStaffProfileDto>>
    {
        private readonly IClinicalProfileRepository _clinicalProfileRepository;

        public GetMyStaffProfileQueryHandler(IClinicalProfileRepository clinicalProfileRepository)
        {
            _clinicalProfileRepository = clinicalProfileRepository;
        }

        public async Task<ResultT<ClinicalStaffProfileDto>> Handle(GetStaffProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _clinicalProfileRepository.GetByUserIdAsync(request.userId, cancellationToken);
            if (profile is null)
                return ClinicalStaffProfileErrors.NotFound(request.userId);

            return new ClinicalStaffProfileDto(profile.Id, profile.UserId, profile.Role.ToString(), profile.IsActive);
        }
    }
}