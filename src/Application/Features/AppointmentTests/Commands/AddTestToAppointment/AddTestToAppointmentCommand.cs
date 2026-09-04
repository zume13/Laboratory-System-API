using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentTests.Commands.AddTestToAppointment
{
    public record AddTestToAppointmentCommand(Guid appointmentId, Guid testCategoryId) : IRequest<ResultT<Guid>>;
}