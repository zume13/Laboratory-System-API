using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.CancelAppointmentTest
{
    public record CancelAppointmentTestCommand(Guid appointmentTestId, Guid appointmentId) : IRequest<Result>;
}
