using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.AppointmentSlot;
using Domain.Aggregates.Laboratory.TestCategory;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Update
{
    public class UpdateAppointmentSlotCommandHandler : IRequestHandler<UpdateAppointmentSlotCommand, Result>
    {
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAppointmentSlotCommandHandler(
            IAppointmentSlotRepository appointmentSlotRepository,
            ITestCategoryRepository testCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _appointmentSlotRepository = appointmentSlotRepository;
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateAppointmentSlotCommand request, CancellationToken cancellationToken)
        {
            var slot = await _appointmentSlotRepository.GetByIdAsync(request.appointmentSlotId, cancellationToken);
            if (slot is null)
                return AppointmentSlotErrors.NotFound(request.appointmentSlotId);

            if (request.testCategoryId is not null)
            {
                var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId.Value, cancellationToken);
                if (category is null)
                    return TestCategoryErrors.NotFound(request.testCategoryId.Value);
            }

            var timeRangeResult = TimeRange.Create(request.startTime, request.endTime);
            if (timeRangeResult.IsFailure)
                return timeRangeResult.Error;

            var updateResult = slot.UpdateDetails(
                request.date,
                timeRangeResult.value,
                request.testCategoryId,
                request.capacity);

            if (updateResult.IsFailure)
                return updateResult.Error;

            _appointmentSlotRepository.Update(slot);

            // once notif system is done, notify the patients if date/time changed and bookedCount > 0

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}