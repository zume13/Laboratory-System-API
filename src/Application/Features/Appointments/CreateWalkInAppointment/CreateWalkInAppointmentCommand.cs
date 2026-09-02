using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.CreateWalkInAppointment
{
    public record CreateWalkInAppointmentCommand(
        Guid patientId,
        Guid appointmentSlotId,
        Guid testCategoryId) : IRequest<ResultT<Guid>>;
}
