using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Queries.GetPastDueUnresolvedAppointments
{
    public class GetPastDueUnresolvedAppointmentsQueryHandler : IRequestHandler<GetPastDueUnresolvedAppointmentsQuery, ResultT<List<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetPastDueUnresolvedAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ResultT<List<AppointmentDto>>> Handle(GetPastDueUnresolvedAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetPastDueUnresolvedAsync(request.asOf, cancellationToken);

            return appointments.Select(a => new AppointmentDto(
                a.Id, a.PatientId, a.AppointmentSlotId, a.Status.ToString(), a.BookingChannel.ToString(), a.CreatedAt, a.ConfirmedAt)).ToList();
        }
    }
}