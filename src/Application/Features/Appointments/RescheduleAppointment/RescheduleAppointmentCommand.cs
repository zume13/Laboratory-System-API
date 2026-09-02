using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.RescheduleAppointment
{
    public record RescheduleAppointmentCommand(
        Guid AppointmentId,
        Guid CurrentAppointmentSlotId,
        Guid NewAppointmentSlotId
        ) : IRequest<Result>; 

}
