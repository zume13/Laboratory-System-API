using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.CancelAppointmentTest
{
    public record CancelAppointmentCommand(Guid appointmentTestId, Guid appointmentId) : IRequest<Result>;
}
