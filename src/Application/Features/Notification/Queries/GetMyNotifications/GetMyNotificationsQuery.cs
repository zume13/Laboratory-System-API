using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Notification.Queries.GetMyNotifications
{
    public record GetMyNotificationsQuery(Guid patientId) : IRequest<ResultT<List<NotificationDto>>>;
}