using Domain.Aggregates.Appointment.Enums;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Commands.CreateOnlineAppointment
{
    public record CreateOnlineAppointmentCommand(
            Guid patientId,
            Guid appointmentSlotId,
            IEnumerable<Guid> testCategoryIds) : IRequest<ResultT<Guid>>;
}
