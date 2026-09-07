using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Queries.GetAllAppointmentsBySlot
{
    public class GetAllAppointmentsBySlotQueryHandler : IRequestHandler<GetAllAppointmentsBySlotQuery, ResultT<List<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAllAppointmentsBySlotQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ResultT<List<AppointmentDto>>> Handle(GetAllAppointmentsBySlotQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAllByAppointmentSlotIdAsync(request.appointmentSlotId, cancellationToken);

            return appointments.Select(a => new AppointmentDto(
                a.Id, a.PatientId, a.AppointmentSlotId, a.Status.ToString(), a.BookingChannel.ToString(), a.CreatedAt, a.ConfirmedAt)).ToList();
        }
    }
}