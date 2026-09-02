using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.PatientProfile;
using Domain.Aggregates.Laboratory.LaboratoryRequest;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Commands.AttachPatientToWalkInRequest
{
    public class AttachPatientToWalkInRequestCommandHandler
        : IRequestHandler<AttachPatientToWalkInRequestCommand, Result>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;
        private readonly IPatientProfileRepository _patientProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AttachPatientToWalkInRequestCommandHandler(
            ILaboratoryRequestRepository laboratoryRequestRepository,
            IPatientProfileRepository patientProfileRepository,
            IUnitOfWork unitOfWork)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
            _patientProfileRepository = patientProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AttachPatientToWalkInRequestCommand request, CancellationToken cancellationToken)
        {
            var labRequest = await _laboratoryRequestRepository.GetByIdAsync(request.laboratoryRequestId, cancellationToken);
            if (labRequest is null)
                return LaboratoryRequestErrors.LaboratoryRequest.NotFound(request.laboratoryRequestId);

            var patientProfile = await _patientProfileRepository.GetByUserIdAsync(request.patientId, cancellationToken);
            if (patientProfile is null)
                return PatientProfileErrors.NotFound(request.patientId);

            var attachResult = labRequest.AttachPatient(request.patientId);
            if (attachResult.IsFailure)
                return attachResult.Error;

            _laboratoryRequestRepository.Update(labRequest);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}