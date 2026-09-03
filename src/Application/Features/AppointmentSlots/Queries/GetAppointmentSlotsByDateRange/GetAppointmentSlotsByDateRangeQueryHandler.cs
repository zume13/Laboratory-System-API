using Application.Abstractions.Repositories;
using Application.Dto;
using Application.Features.AppointmentSlots;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetAppointmentSlotsByDateRange
{

    public class GetAppointmentSlotsByDateRangeQueryHandler : IRequestHandler<GetAppointmentSlotsByDateRangeQuery, ResultT<List<AppointmentSlotDto>>>
    {
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        public GetAppointmentSlotsByDateRangeQueryHandler(IAppointmentSlotRepository repo) => _appointmentSlotRepository = repo;

        public async Task<ResultT<List<AppointmentSlotDto>>> Handle(GetAppointmentSlotsByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var slots = await _appointmentSlotRepository.GetByDateRangeAsync(request.from, request.to, cancellationToken);
            return slots.Select(AppointmentSlotMapper.ToDto).ToList();
        }
    }
}