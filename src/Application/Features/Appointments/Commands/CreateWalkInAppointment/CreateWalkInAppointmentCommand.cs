using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.CreateWalkInAppointment
{
    public record CreateWalkInAppointmentCommand(
        Guid patientId,
        Guid appointmentSlotId,
        IEnumerable<Guid> testCategoryIds) : IRequest<ResultT<Guid>>;
}
