using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Queries.SearchActivityLogs
{
    public class SearchActivityLogsQueryHandler : IRequestHandler<SearchActivityLogsQuery, ResultT<PagedResult<ActivityLogDto>>>
    {
        private readonly IActivityLogRepository _activityLogRepository;

        public SearchActivityLogsQueryHandler(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public async Task<ResultT<PagedResult<ActivityLogDto>>> Handle(SearchActivityLogsQuery request, CancellationToken cancellationToken)
        {
            DateTime? from = request.from.HasValue
                ? DateTime.SpecifyKind(request.from.Value, DateTimeKind.Utc)
                : null;

            DateTime? to = null;
            if (request.to.HasValue)
            {
                var toValue = DateTime.SpecifyKind(request.to.Value, DateTimeKind.Utc);
                to = toValue.TimeOfDay == TimeSpan.Zero ? toValue.AddDays(1).AddTicks(-1) : toValue;
            }

            var page = await _activityLogRepository.SearchAsync(
                request.userId, request.target, request.severity, from, to,
                request.page, request.pageSize, cancellationToken);

            var items = page.Items
                .Select(l => new ActivityLogDto(l.Id, l.UserId, l.Action, l.Target, l.Severity.ToString(), l.Timestamp))
                .ToList();

            return new PagedResult<ActivityLogDto>(items, page.TotalCount, page.Page, page.PageSize);
        }
    }
}