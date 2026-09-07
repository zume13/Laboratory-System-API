using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.Commands.CompleteLabRequest
{
    public class CompleteLabRequestCommandHandler : IRequestHandler<CompleteLabRequestCommand, Result>
    {
        private readonly ILabOrderRepository _labOrderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteLabRequestCommandHandler(ILabOrderRepository labOrderRepository, IUnitOfWork unitOfWork)
        {
            _labOrderRepository = labOrderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CompleteLabRequestCommand request, CancellationToken cancellationToken)
        {
            var labOrder = await _labOrderRepository.GetLabOrderWithLabRequestForUpdateAsync(request.LabOrderId, cancellationToken);
            if (labOrder is null)
                return LaboratoryOrderErrors.LabOrder.NotFound;

            var result = labOrder.CompleteRequest(request.RequestId);
            if (result.IsFailure)
                return result.Error;

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}