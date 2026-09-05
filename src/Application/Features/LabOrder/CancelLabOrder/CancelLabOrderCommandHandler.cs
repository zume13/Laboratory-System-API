using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.CancelLabOrder
{
    public class CancelLabOrderCommandHandler : IRequestHandler<CancelLabOrderCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILabOrderRepository _labOrderRepository;

        public CancelLabOrderCommandHandler(IUnitOfWork unitOfWork, ILabOrderRepository labOrderRepository)
        {
            _unitOfWork = unitOfWork;
            _labOrderRepository = labOrderRepository;
        }
        public async Task<Result> Handle(CancelLabOrderCommand request, CancellationToken cancellationToken)
        {
            var labOrder = await _labOrderRepository.GetLabOrderWithLabRequestAsync(request.LabOrderId);

            if(labOrder is null)
                return LaboratoryOrderErrors.LabOrder.NotFound; 

            var result = labOrder.Cancel();

            if (result.IsFailure)
                return result;

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
                return saveResult;

            return Result.Success();
        }
    }
}
