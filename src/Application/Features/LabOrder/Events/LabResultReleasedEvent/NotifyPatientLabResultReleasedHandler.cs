using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Communications.Enums;
using Domain.Aggregates.Laboratory.LaboratoryOrder.Events;
using SharedKernel.DomainEvent;

namespace Application.Features.LabOrder.Events.LabResultReleasedEvent
{
    public class NotifyPatientLabResultReleasedHandler : IDomainEventHandler<Domain.Aggregates.Laboratory.LaboratoryOrder.Events.LabResultReleasedEvent>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotifyPatientLabResultReleasedHandler(
            ITestCategoryRepository testCategoryRepository,
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork)
        {
            _testCategoryRepository = testCategoryRepository;
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Domain.Aggregates.Laboratory.LaboratoryOrder.Events.LabResultReleasedEvent domainEvent, CancellationToken cancellationToken)
        {
            var category = await _testCategoryRepository.GetByIdAsync(domainEvent.TestCategoryId, cancellationToken);
            var testName = category?.Name.value ?? "your test";

            var notificationResult = Domain.Aggregates.Communications.Notification.Notification.Dispatch(
                domainEvent.PatientId,
                NotificationChannel.InPortal,
                $"Your {testName} results are now available.",
                domainEvent.RequestId);

            if (notificationResult.IsFailure)
                return;

            await _notificationRepository.AddAsync(notificationResult.value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}