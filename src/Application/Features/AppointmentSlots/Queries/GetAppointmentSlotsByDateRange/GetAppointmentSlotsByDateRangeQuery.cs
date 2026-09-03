using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AppointmentSlots.Queries.GetAppointmentSlotsByDateRange { 
public record GetAppointmentSlotsByDateRangeQuery(DateTime from, DateTime to) : IRequest<ResultT<List<AppointmentSlotDto>>>;
}