using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.SlotCapacity;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SlotCapacity.Queries.GetSlotCapacityConfigByTestCategoryId
{
    public class GetSlotCapacityConfigByTestCategoryIdQueryHandler
        : IRequestHandler<GetSlotCapacityConfigByTestCategoryIdQuery, ResultT<SlotCapacityConfigDto>>
    {
        private readonly ISlotCapacityRepository _slotCapacityRepository;

        public GetSlotCapacityConfigByTestCategoryIdQueryHandler(ISlotCapacityRepository slotCapacityRepository)
        {
            _slotCapacityRepository = slotCapacityRepository;
        }

        public async Task<ResultT<SlotCapacityConfigDto>> Handle(GetSlotCapacityConfigByTestCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var config = await _slotCapacityRepository.GetByTestCategoryIdAsync(request.testCategoryId, cancellationToken);
            if (config is null)
                return SlotCapacityErrors.NotFound(request.testCategoryId);

            return new SlotCapacityConfigDto(config.Id, config.TestCategoryId, config.MaxDailyBookings, config.MaxPerSlot);
        }
    }
}