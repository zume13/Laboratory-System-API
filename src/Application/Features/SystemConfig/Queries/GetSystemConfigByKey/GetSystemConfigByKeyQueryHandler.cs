using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Monitoring.SystemConfig;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SystemConfig.Queries.GetSystemConfigByKey
{
    public class GetSystemConfigByKeyQueryHandler : IRequestHandler<GetSystemConfigByKeyQuery, ResultT<SystemConfigDto>>
    {
        private readonly ISystemConfigRepository _systemConfigRepository;

        public GetSystemConfigByKeyQueryHandler(ISystemConfigRepository systemConfigRepository)
        {
            _systemConfigRepository = systemConfigRepository;
        }

        public async Task<ResultT<SystemConfigDto>> Handle(GetSystemConfigByKeyQuery request, CancellationToken cancellationToken)
        {
            var config = await _systemConfigRepository.GetByKeyAsync(request.key, cancellationToken);
            if (config is null)
                return SystemConfigErrors.NotFound(request.key);

            return new SystemConfigDto(config.Id, config.Key, config.Value, config.UpdatedAt);
        }
    }
}