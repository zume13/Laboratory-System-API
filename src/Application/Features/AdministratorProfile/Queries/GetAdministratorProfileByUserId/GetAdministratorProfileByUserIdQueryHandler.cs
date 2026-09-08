using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Identity.AdministratorProfile;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Queries.GetAdministratorProfileByUserId
{
    public class GetAdministratorProfileByUserIdQueryHandler : IRequestHandler<GetAdministratorProfileByUserIdQuery, ResultT<AdministratorProfileDto>>
    {
        private readonly IAdministratorProfileRepository _administratorProfileRepository;

        public GetAdministratorProfileByUserIdQueryHandler(IAdministratorProfileRepository administratorProfileRepository)
        {
            _administratorProfileRepository = administratorProfileRepository;
        }

        public async Task<ResultT<AdministratorProfileDto>> Handle(GetAdministratorProfileByUserIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await _administratorProfileRepository.GetByUserIdAsync(request.userId, cancellationToken);
            if (profile is null)
                return AdministratorStaffProfileErrors.NotFound(request.userId);

            return new AdministratorProfileDto(profile.Id, profile.UserId);
        }
    }
}