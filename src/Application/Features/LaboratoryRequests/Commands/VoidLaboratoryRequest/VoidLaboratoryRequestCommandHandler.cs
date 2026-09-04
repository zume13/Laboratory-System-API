using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.LaboratoryOrder.LaboratoryRequest;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LaboratoryRequests.Commands.VoidLaboratoryRequest
{
    public class VoidLaboratoryRequestCommandHandler : IRequestHandler<VoidLaboratoryRequestCommand, Result>
    {
        private readonly ILaboratoryRequestRepository _laboratoryRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VoidLaboratoryRequestCommandHandler(
            ILaboratoryRequestRepository laboratoryRequestRepository,
            IUnitOfWork unitOfWork)
        {
            _laboratoryRequestRepository = laboratoryRequestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(VoidLaboratoryRequestCommand request, CancellationToken cancellationToken)
        {
            var labRequest = await _laboratoryRequestRepository.GetByIdAsync(request.laboratoryRequestId, cancellationToken);
            if (labRequest is null)
                return LaboratoryRequestErrors.LaboratoryRequest.NotFound(request.laboratoryRequestId);

            var voidResult = labRequest.Void();
            if (voidResult.IsFailure)
                return voidResult.Error;

            _laboratoryRequestRepository.Update(labRequest);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}