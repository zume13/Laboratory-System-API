using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Queries.GetActivityLogsByDate
{
    public record GetActivityLogsByDateQuery(DateTime date) : IRequest<ResultT<List<ActivityLogDto>>>;
}