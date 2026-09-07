using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Queries.GetActivityLogsByDate
{
    public class GetActivityLogsByDateQueryHandler : IRequestHandler<GetActivityLogsByDateQuery, ResultT<List<ActivityLogDto>>>
    {
        private readonly IActivityLogRepository _activityLogRepository;

        public GetActivityLogsByDateQueryHandler(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public async Task<ResultT<List<ActivityLogDto>>> Handle(GetActivityLogsByDateQuery request, CancellationToken cancellationToken)
        {
            var startOfDay = DateTime.SpecifyKind(request.date.Date, DateTimeKind.Utc);
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            var logs = await _activityLogRepository.GetByDateRangeAsync(startOfDay, endOfDay, cancellationToken);

            return logs.Select(l => new ActivityLogDto(l.Id, l.UserId, l.Action, l.Target, l.Severity.ToString(), l.Timestamp)).ToList();
        }
    }
}