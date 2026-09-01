using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.CreateWalkInAppointment
{
    public record CreateWalkInAppointmentCommand() : IRequest<ResultT<Guid>>;
}
