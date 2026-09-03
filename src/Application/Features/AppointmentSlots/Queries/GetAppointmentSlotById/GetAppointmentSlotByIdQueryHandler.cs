using Application.Abstractions.Repositories;
using Application.Dto;
using Application.Features.AppointmentSlots;
using Domain.Aggregates.AppointmentSlot;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetAppointmentSlotById
{
    public class GetAppointmentSlotByIdQueryHandler : IRequestHandler<GetAppointmentSlotByIdQuery, ResultT<AppointmentSlotDto>>
    {
        private readonly IAppointmentSlotRepository _appointmentSlotRepository;
        public GetAppointmentSlotByIdQueryHandler(IAppointmentSlotRepository repo) => _appointmentSlotRepository = repo;

        public async Task<ResultT<AppointmentSlotDto>> Handle(GetAppointmentSlotByIdQuery request, CancellationToken cancellationToken)
        {
            var slot = await _appointmentSlotRepository.GetByIdAsync(request.appointmentSlotId, cancellationToken);
            if (slot is null) return AppointmentSlotErrors.NotFound(request.appointmentSlotId);
            return AppointmentSlotMapper.ToDto(slot);
        }
    }
}