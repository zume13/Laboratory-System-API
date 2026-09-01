using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.AppointmentSlot;
using Domain.Aggregates.Laboratory.TestCategory;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Create
{
    public class CreateAppointmentSlotCommandHandler : IRequestHandler<CreateAppointmentSlotCommand, ResultT<Guid>>
    {
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentSlotCommandHandler(
            IAppointmentSlotRepository appointmentSlotRepository,
            ITestCategoryRepository testCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _appointmentSlotRepository = appointmentSlotRepository;
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultT<Guid>> Handle(CreateAppointmentSlotCommand request, CancellationToken cancellationToken)
        {
            if (request.testCategoryId is not null)
            {
                var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId.Value, cancellationToken);
                if (category is null)
                    return TestCategoryErrors.NotFound(request.testCategoryId.Value);
            }

            var timeRangeResult = TimeRange.Create(request.startTime, request.endTime);
            if (timeRangeResult.IsFailure)
                return timeRangeResult.Error;

            var slotResult = AppointmentSlot.Create(
                request.date,
                timeRangeResult.value,
                request.testCategoryId,
                request.capacity,
                request.configuredByStaffId);

            if (slotResult.IsFailure)
                return slotResult.Error;

            await _appointmentSlotRepository.AddAsync(slotResult.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return slotResult.value.Id;
        }
    }
}