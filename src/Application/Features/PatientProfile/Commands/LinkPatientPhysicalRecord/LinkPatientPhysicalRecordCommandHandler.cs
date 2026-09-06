using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.PatientProfile;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.PatientProfile.Commands.LinkPatientPhysicalRecord
{
    public class LinkPatientPhysicalRecordCommandHandler : IRequestHandler<LinkPatientPhysicalRecordCommand, Result>
    {
        private readonly IPatientProfileRepository _patientProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LinkPatientPhysicalRecordCommandHandler(IPatientProfileRepository patientProfileRepository, IUnitOfWork unitOfWork)
        {
            _patientProfileRepository = patientProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(LinkPatientPhysicalRecordCommand request, CancellationToken cancellationToken)
        {
            var profile = await _patientProfileRepository.GetByUserIdAsync(request.patientUserId, cancellationToken);
            if (profile is null)
                return PatientProfileErrors.NotFound(request.patientUserId);

            var result = profile.LinkPhysicalRecord(request.physicalPatientId);
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