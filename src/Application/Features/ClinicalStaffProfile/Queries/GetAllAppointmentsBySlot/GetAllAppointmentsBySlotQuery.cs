using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Queries.GetAllAppointmentsBySlot
{
    public record GetAllAppointmentsBySlotQuery(Guid appointmentSlotId) : IRequest<ResultT<List<AppointmentDto>>>;
}