using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Identity.PatientProfile;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Queries.GetMyPatientProfile
{
    public class GetMyPatientProfileQueryHandler : IRequestHandler<GetMyPatientProfileQuery, ResultT<PatientProfileDto>>
    {
        private readonly IPatientProfileRepository _patientProfileRepository;

        public GetMyPatientProfileQueryHandler(IPatientProfileRepository patientProfileRepository)
        {
            _patientProfileRepository = patientProfileRepository;
        }

        public async Task<ResultT<PatientProfileDto>> Handle(GetMyPatientProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _patientProfileRepository.GetByUserIdAsync(request.userId, cancellationToken);
            if (profile is null)
                return PatientProfileErrors.NotFound(request.userId);

            return new PatientProfileDto(profile.Id, profile.UserId, profile.DateOfBirth, profile.Sex.ToString(), profile.PhysicalPatientId, profile.ConsentAccepted);
        }
    }
}