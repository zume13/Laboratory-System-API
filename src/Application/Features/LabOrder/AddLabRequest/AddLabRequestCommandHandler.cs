using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.LabOrder.AddLabRequest
{
    public class AddLabRequestCommandHandler : IRequestHandler<AddLabRequestCommand, Result>
    {
        private readonly ILabOrderRepository _labOrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddLabRequestCommandHandler(ILabOrderRepository labOrderRepository, IUnitOfWork unitOfWork)
        {
            _labOrderRepository = labOrderRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(AddLabRequestCommand request, CancellationToken cancellationToken)
        {
            var labOrder = await _labOrderRepository.GetLabOrderWithLabRequestAsync(request.LabOrderId);

            if (labOrder is null)
                return LaboratoryOrderErrors.LabOrder.NotFound;

            var addResult = labOrder.AddRequest(request.TestCategoryId);

            if (addResult.IsFailure)
                return addResult.Error;

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }   
    }
}
