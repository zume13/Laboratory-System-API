using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.MarkAppointmentNoShow
{
    public record MarkAppointmentNoShowCommand(Guid AppointmentId) : IRequest<Result>;
}
