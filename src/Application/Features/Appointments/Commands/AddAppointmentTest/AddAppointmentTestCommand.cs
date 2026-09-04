using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.AddAppointmentTest
{
    public record AddAppointmentTestCommand(Guid testCategoryId, Guid appointmentId) : IRequest<Result>;
}
