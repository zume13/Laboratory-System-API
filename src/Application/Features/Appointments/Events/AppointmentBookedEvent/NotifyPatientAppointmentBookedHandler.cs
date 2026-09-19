using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Appointment.Events;
using Domain.Aggregates.Communications.Enums;
using SharedKernel.DomainEvent;

namespace Application.Features.Appointments.Events.AppointmentBookedEvent
{
    public class NotifyPatientAppointmentBookedHandler : IDomainEventHandler<Domain.Aggregates.Appointment.Events.AppointmentBookedEvent>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotifyPatientAppointmentBookedHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Domain.Aggregates.Appointment.Events.AppointmentBookedEvent domainEvent, CancellationToken cancellationToken)
        {
            var notificationResult = Domain.Aggregates.Communications.Notification.Notification.Dispatch(
                domainEvent.PatientId,
                NotificationChannel.InPortal,
                "Your appointment has been booked.");

            if (notificationResult.IsFailure)
                return;

            await _notificationRepository.AddAsync(notificationResult.value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}