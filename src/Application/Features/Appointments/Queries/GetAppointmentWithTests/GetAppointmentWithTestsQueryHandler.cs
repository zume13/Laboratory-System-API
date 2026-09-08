using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Appointment;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Queries.GetAppointmentWithTests
{
    public class GetAppointmentWithTestsQueryHandler : IRequestHandler<GetAppointmentWithTestsQuery, ResultT<AppointmentWithTestsDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentWithTestsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ResultT<AppointmentWithTestsDto>> Handle(GetAppointmentWithTestsQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAppointmentWithAppointmentTestAsync(request.appointmentId, cancellationToken);
            if (appointment is null)
                return AppointmentErrors.NotFound;

            var tests = appointment.Tests
                .Select(t => new AppointmentTestDto(t.Id, t.TestCategoryId, t.isApproved))
                .ToList();

            return new AppointmentWithTestsDto(appointment.Id, appointment.PatientId, appointment.AppointmentSlotId, appointment.Status.ToString(), tests);
        }
    }
}