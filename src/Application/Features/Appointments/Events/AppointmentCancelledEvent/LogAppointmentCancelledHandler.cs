using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment.Events;
using ActLog = Domain.Aggregates.Monitoring.ActivityLog.ActivityLog;
using Domain.Aggregates.Monitoring.Enums;
using SharedKernel.DomainEvent;

namespace Application.Features.Appointments.Events.AppointmentCancelledEvent
{
    public class LogAppointmentCancelledHandler : IDomainEventHandler<Domain.Aggregates.Appointment.Events.AppointmentCancelledEvent>
    {
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogAppointmentCancelledHandler(IActivityLogRepository activityLogRepository, IUnitOfWork unitOfWork)
        {
            _activityLogRepository = activityLogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Domain.Aggregates.Appointment.Events.AppointmentCancelledEvent domainEvent, CancellationToken cancellationToken)
        {
            var logResult = ActLog.Record(
                domainEvent.PatientId,
                "AppointmentCancelled",
                $"Appointment:{domainEvent.AppointmentId}",
                LogSeverity.Info);

            if (logResult.IsFailure)
                return;

            await _activityLogRepository.AddAsync(logResult.value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}