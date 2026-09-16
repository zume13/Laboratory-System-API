using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.SystemConfig;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SystemConfig.Commands.CreateSystemConfig
{
    public class CreateSystemConfigCommandHandler : IRequestHandler<CreateSystemConfigCommand, ResultT<Guid>>
    {
        private readonly ISystemConfigRepository _systemConfigRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSystemConfigCommandHandler(ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            _systemConfigRepository = systemConfigRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultT<Guid>> Handle(CreateSystemConfigCommand request, CancellationToken cancellationToken)
        {
            var existing = await _systemConfigRepository.GetByKeyAsync(request.key, cancellationToken);
            if (existing is not null)
                return SystemConfigErrors.AlreadyExists(request.key);

            var configResult = Domain.Aggregates.Monitoring.SystemConfig.SystemConfig.Set(request.key, request.value);
            if (configResult.IsFailure)
                return configResult.Error;

            await _systemConfigRepository.AddAsync(configResult.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return configResult.value.Id;
        }
    }
}