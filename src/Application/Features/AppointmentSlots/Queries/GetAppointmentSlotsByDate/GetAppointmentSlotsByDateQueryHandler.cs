using Application.Abstractions.Repositories;
using Application.Dto;
using Application.Features.AppointmentSlots;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetAppointmentSlotsByDate
{
    public class GetAppointmentSlotsByDateQueryHandler : IRequestHandler<GetAppointmentSlotsByDateQuery, ResultT<List<AppointmentSlotDto>>>
    {
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        public GetAppointmentSlotsByDateQueryHandler(IAppointmentSlotRepository repo) => _appointmentSlotRepository = repo;

        public async Task<ResultT<List<AppointmentSlotDto>>> Handle(GetAppointmentSlotsByDateQuery request, CancellationToken cancellationToken)
        {
            var slots = await _appointmentSlotRepository.GetByDateAsync(request.date, cancellationToken);
            return slots.Select(AppointmentSlotMapper.ToDto).ToList();
        }
    }
}