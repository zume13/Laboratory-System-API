using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.RemoveAppointmentTest
{
    public record RemoveAppointmentTestCommand(Guid testCategoryId, Guid appointmentId) : IRequest<Result>;
}

