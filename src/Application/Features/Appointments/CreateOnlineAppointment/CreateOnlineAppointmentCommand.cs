using Domain.Aggregates.Appointment.Enums;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.CreateOnlineAppointment
{
    public record CreateOnlineAppointmentCommand(
            Guid patientId,
            Guid appointmentSlotId,
            Guid testCategoryId) : IRequest<ResultT<Guid>>;
}
