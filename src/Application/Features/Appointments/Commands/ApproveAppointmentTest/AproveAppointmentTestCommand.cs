using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.ApproveAppointmentTest
{
    public record AproveAppointmentTestCommand(Guid appointmentTestId, Guid appointmentId) : IRequest<Result>;
}
