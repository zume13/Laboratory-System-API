using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.SlotCapacity;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SlotCapacity.Commands.Update
{
    public class UpdateSlotCapacityLimitsCommandHandler : IRequestHandler<UpdateSlotCapacityLimitsCommand, Result>
    {
        private readonly ISlotCapacityRepository _slotCapacityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSlotCapacityLimitsCommandHandler(
            ISlotCapacityRepository slotCapacityRepository,
            IUnitOfWork unitOfWork)
        {
            _slotCapacityRepository = slotCapacityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateSlotCapacityLimitsCommand request, CancellationToken cancellationToken)
        {
            var config = await _slotCapacityRepository.GetByTestCategoryIdAsync(request.testCategoryId, cancellationToken);
            if (config is null)
                return SlotCapacityErrors.NotFound(request.testCategoryId);

            var updateResult = config.UpdateLimits(request.maxDailyBookings, request.maxPerSlot);
            if (updateResult.IsFailure)
                return updateResult.Error;

            _slotCapacityRepository.Update(config);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}