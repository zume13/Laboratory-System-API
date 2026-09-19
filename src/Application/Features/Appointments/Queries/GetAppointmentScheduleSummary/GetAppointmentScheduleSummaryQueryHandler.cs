using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Appointments.Queries.GetAppointmentScheduleSummary
{
    public class GetAppointmentScheduleSummaryQueryHandler : IRequestHandler<GetAppointmentScheduleSummaryQuery, ResultT<AppointmentScheduleSummaryDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentScheduleSummaryQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ResultT<AppointmentScheduleSummaryDto>> Handle(GetAppointmentScheduleSummaryQuery request, CancellationToken cancellationToken)
        {
            var summary = await _appointmentRepository.GetScheduleSummaryByDateAsync(request.date, cancellationToken);
            return summary;
        }
    }
}