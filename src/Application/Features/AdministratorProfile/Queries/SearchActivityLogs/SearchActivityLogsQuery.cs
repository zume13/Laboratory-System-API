using Application.Abstractions.Base;
using Application.Dto;
using Domain.Aggregates.Monitoring.Enums;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Queries.SearchActivityLogs
{
    public record SearchActivityLogsQuery(
        Guid? userId,
        string? target,
        LogSeverity? severity,
        DateTime? from,
        DateTime? to,
        int page,
        int pageSize) : IRequest<ResultT<PagedResult<ActivityLogDto>>>;
}