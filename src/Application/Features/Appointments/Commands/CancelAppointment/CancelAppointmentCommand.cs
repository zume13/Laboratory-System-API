using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.CancelAppointment
{
    public record CancelAppointmentCommand(Guid AppointmentId) : IRequest<Result>;
}
