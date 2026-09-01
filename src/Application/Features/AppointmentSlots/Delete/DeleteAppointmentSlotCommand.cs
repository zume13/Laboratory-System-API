using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Delete
{
    public record DeleteAppointmentSlotCommand(Guid appointmentSlotId) : IRequest<Result>;
}