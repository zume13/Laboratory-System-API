using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.TestCategory;
using Domain.Aggregates.SlotCapacity;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SlotCapacity.Commands.Create
{
    public class CreateSlotCapacityConfigCommandHandler : IRequestHandler<CreateSlotCapacityConfigCommand, ResultT<Guid>>
    {
        private readonly ISlotCapacityRepository _slotCapacityRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSlotCapacityConfigCommandHandler(
            ISlotCapacityRepository slotCapacityRepository,
            ITestCategoryRepository testCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _slotCapacityRepository = slotCapacityRepository;
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultT<Guid>> Handle(CreateSlotCapacityConfigCommand request, CancellationToken cancellationToken)
        {
            var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId, cancellationToken);
            if (category is null)
                return TestCategoryErrors.NotFound(request.testCategoryId);

            var existing = await _slotCapacityRepository.GetByTestCategoryIdAsync(request.testCategoryId, cancellationToken);
            if (existing is not null)
                return SlotCapacityErrors.AlreadyExists(request.testCategoryId);

            var configResult = SlotCapacityConfig.Create(request.testCategoryId, request.maxDailyBookings, request.maxPerSlot);
            if (configResult.IsFailure)
                return configResult.Error;

            await _slotCapacityRepository.AddAsync(configResult.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return configResult.value.Id;
        }
    }
}