using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.CompleteLabOrder
{
    public class CompleteLabOrderCommandHandler : IRequestHandler<CompleteLabOrderCommand, Result>
    {
        private readonly ILabOrderRepository _labOrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CompleteLabOrderCommandHandler(ILabOrderRepository labOrderRepository, IUnitOfWork unitOfWork)
        {
            _labOrderRepository = labOrderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(CompleteLabOrderCommand request, CancellationToken cancellationToken)
        {
            var labOrder = await _labOrderRepository.GetLabOrderWithLabRequestAsync(request.LabOrderId);

            if (labOrder is null)
                return LaboratoryOrderErrors.LabOrder.NotFound;

            var completeResult = labOrder.Complete();

            if (completeResult.IsFailure)
                return completeResult.Error;

            var saveResult = await _unitOfWork.SaveChangesAsync();

            if(saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }   
    }
}
