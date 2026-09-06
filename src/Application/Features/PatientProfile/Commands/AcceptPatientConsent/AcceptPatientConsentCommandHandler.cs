using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.PatientProfile;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Commands.AcceptPatientConsent
{
    public class AcceptPatientConsentCommandHandler : IRequestHandler<AcceptPatientConsentCommand, Result>
    {
        private readonly IPatientProfileRepository _patientProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AcceptPatientConsentCommandHandler(IPatientProfileRepository patientProfileRepository, IUnitOfWork unitOfWork)
        {
            _patientProfileRepository = patientProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AcceptPatientConsentCommand request, CancellationToken cancellationToken)
        {
            var profile = await _patientProfileRepository.GetByUserIdAsync(request.patientUserId, cancellationToken);
            if (profile is null)
                return PatientProfileErrors.NotFound(request.patientUserId);

            var result = profile.AcceptConsent();
            if (result.IsFailure)
                return result.Error;

            _patientProfileRepository.Update(profile);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}