using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.LaboratoryOrder.Events;
using ActLog = Domain.Aggregates.Monitoring.ActivityLog.ActivityLog;
using Domain.Aggregates.Monitoring.Enums;
using SharedKernel.DomainEvent;

namespace Application.Features.LabOrder.Events.LabResultReleasedEvent
{
    public class LogLabResultReleasedHandler : IDomainEventHandler<Domain.Aggregates.Laboratory.LaboratoryOrder.Events.LabResultReleasedEvent>
    {
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogLabResultReleasedHandler(IActivityLogRepository activityLogRepository, IUnitOfWork unitOfWork)
        {
            _activityLogRepository = activityLogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Domain.Aggregates.Laboratory.LaboratoryOrder.Events.LabResultReleasedEvent domainEvent, CancellationToken cancellationToken)
        {
            var logResult = ActLog.Record(
                domainEvent.PatientId,
                "LabResultReleased",
                $"LaboratoryRequest:{domainEvent.RequestId}",
                LogSeverity.Info);

            if (logResult.IsFailure)
                return;

            await _activityLogRepository.AddAsync(logResult.value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}