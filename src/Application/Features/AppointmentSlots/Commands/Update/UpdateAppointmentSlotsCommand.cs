using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Commands.Update
{
    public record UpdateAppointmentSlotCommand(
        Guid appointmentSlotId,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        Guid? testCategoryId,
        int capacity)
        : IRequest<Result>;
}
