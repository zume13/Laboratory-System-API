using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Commands.Delete
{
    public record DeleteAppointmentSlotCommand(Guid appointmentSlotId) : IRequest<Result>;
}