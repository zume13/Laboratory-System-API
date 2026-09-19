using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Monitoring.SystemConfig;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SystemConfig.Commands.UpdateSystemConfigValue
{
    public class UpdateSystemConfigValueCommandHandler : IRequestHandler<UpdateSystemConfigValueCommand, Result>
    {
        private readonly ISystemConfigRepository _systemConfigRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSystemConfigValueCommandHandler(ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            _systemConfigRepository = systemConfigRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateSystemConfigValueCommand request, CancellationToken cancellationToken)
        {
            var config = await _systemConfigRepository.GetByKeyAsync(request.key, cancellationToken);
            if (config is null)
                return SystemConfigErrors.NotFound(request.key);

            var updateResult = config.UpdateValue(request.value);
            if (updateResult.IsFailure)
                return updateResult.Error;

            _systemConfigRepository.Update(config);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}