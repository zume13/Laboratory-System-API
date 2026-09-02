using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.CancelAppointment
{
    public record CancelAppointmentCommand(Guid AppointmentId) : IRequest<Result>;
}
