using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetSlotUtilizationByDate
{
    public class GetSlotUtilizationByDateQueryHandler : IRequestHandler<GetSlotUtilizationByDateQuery, ResultT<List<SlotUtilizationDto>>>
    {
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        private readonly ISlotCapacityRepository _slotCapacityRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;

        public GetSlotUtilizationByDateQueryHandler(
            IAppointmentSlotRepository appointmentSlotRepository,
            ISlotCapacityRepository slotCapacityRepository,
            ITestCategoryRepository testCategoryRepository)
        {
            _appointmentSlotRepository = appointmentSlotRepository;
            _slotCapacityRepository = slotCapacityRepository;
            _testCategoryRepository = testCategoryRepository;
        }

        public async Task<ResultT<List<SlotUtilizationDto>>> Handle(GetSlotUtilizationByDateQuery request, CancellationToken cancellationToken)
        {
            var slots = await _appointmentSlotRepository.GetByDateAsync(request.date, cancellationToken);

            var grouped = slots
                .Where(s => s.TestCategoryId is not null)
                .GroupBy(s => s.TestCategoryId!.Value);

            var results = new List<SlotUtilizationDto>();

            foreach (var group in grouped)
            {
                var testCategoryId = group.Key;
                var totalCapacity = group.Sum(s => s.Capacity);
                var totalBooked = group.Sum(s => s.BookedCount);

                var category = await _testCategoryRepository.GetByIdAsync(testCategoryId, cancellationToken);
                var config = await _slotCapacityRepository.GetByTestCategoryIdAsync(testCategoryId, cancellationToken);

                decimal? percentUsed = config is not null && config.MaxDailyBookings > 0
                    ? Math.Round((decimal)totalBooked / config.MaxDailyBookings * 100, 1)
                    : null;

                results.Add(new SlotUtilizationDto(
                    testCategoryId,
                    category?.Name.value ?? "Unknown",
                    totalCapacity,
                    totalBooked,
                    config?.MaxDailyBookings,
                    percentUsed));
            }

            return results;
        }
    }
}