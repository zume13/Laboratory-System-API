using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.RemoveLabRequest
{
    public class RemoveLabRequestCommandHandler : IRequestHandler<RemoveLabRequestCommand, Result>
    {
        private readonly ILabOrderRepository _labOrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveLabRequestCommandHandler(ILabOrderRepository labOrderRepository, IUnitOfWork unitOfWork)
        {
            _labOrderRepository = labOrderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(RemoveLabRequestCommand request, CancellationToken cancellationToken)
        {
            var labOrder = await _labOrderRepository.GetLabOrderWithLabRequestAsync(request.LabOrderId);

            if (labOrder is null)
                return LaboratoryOrderErrors.LabOrder.NotFound;

            var removeResult = labOrder.RemoveRequest(request.TestCategory);

            if (removeResult.IsFailure)
                return removeResult.Error;

            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }       
    }
}
