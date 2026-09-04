using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentTests.Commands.RemoveTestFromAppointment
{
    public record RemoveTestFromAppointmentCommand(Guid appointmentId, Guid appointmentTestId) : IRequest<Result>;
}