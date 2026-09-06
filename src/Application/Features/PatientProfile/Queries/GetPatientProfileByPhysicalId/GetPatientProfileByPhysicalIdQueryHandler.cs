using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Identity.PatientProfile;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Queries.GetPatientProfileByPhysicalId
{
    public class GetPatientProfileByPhysicalIdQueryHandler : IRequestHandler<GetPatientProfileByPhysicalIdQuery, ResultT<PatientProfileDto>>
    {
        private readonly IPatientProfileRepository _patientProfileRepository;

        public GetPatientProfileByPhysicalIdQueryHandler(IPatientProfileRepository patientProfileRepository)
        {
            _patientProfileRepository = patientProfileRepository;
        }

        public async Task<ResultT<PatientProfileDto>> Handle(GetPatientProfileByPhysicalIdQuery request, CancellationToken cancellationToken)
        {
            var profile = await _patientProfileRepository.GetByPhysicalPatientIdAsync(request.physicalPatientId, cancellationToken);
            if (profile is null)
                return PatientProfileErrors.NotFoundByPhysicalId(Guid.Empty);

            return new PatientProfileDto(profile.Id, profile.UserId, profile.DateOfBirth, profile.Sex.ToString(), profile.PhysicalPatientId, profile.ConsentAccepted);
        }
    }
}