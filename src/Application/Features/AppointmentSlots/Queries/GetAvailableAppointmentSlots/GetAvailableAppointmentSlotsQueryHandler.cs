using Application.Abstractions.Repositories;
using Application.Dto;
using Application.Features.AppointmentSlots;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetAvailableAppointmentSlots
{
    public class GetAvailableAppointmentSlotsQueryHandler : IRequestHandler<GetAvailableAppointmentSlotsQuery, ResultT<List<PublicAppointmentSlotDto>>>
    {
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        public GetAvailableAppointmentSlotsQueryHandler(IAppointmentSlotRepository repo) => _appointmentSlotRepository = repo;

        public async Task<ResultT<List<PublicAppointmentSlotDto>>> Handle(GetAvailableAppointmentSlotsQuery request, CancellationToken cancellationToken)
        {
            var slots = await _appointmentSlotRepository.GetAvailableByDateAndCategoryAsync(request.date, request.testCategoryId, cancellationToken);
            return slots.Select(AppointmentSlotMapper.ToPublicDto).ToList();
        }
    }
}