using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Notification.Queries.GetMyNotifications
{
    public class GetMyNotificationsQueryHandler : IRequestHandler<GetMyNotificationsQuery, ResultT<List<NotificationDto>>>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetMyNotificationsQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ResultT<List<NotificationDto>>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetByPatientIdAsync(request.patientId, cancellationToken);

            return notifications.Select(n => new NotificationDto(
                n.Id, n.Channel.ToString(), n.Status.ToString(), n.Message, n.LabResultId, n.SentAt, n.CreatedAt)).ToList();
        }
    }
}