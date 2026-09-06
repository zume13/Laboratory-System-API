using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.AdministratorProfile;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Commands.UpdateAdministratorPermissions
{
    public class UpdateAdministratorPermissionsCommandHandler : IRequestHandler<UpdateAdministratorPermissionsCommand, Result>
    {
        private readonly IAdministratorProfileRepository _administratorProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAdministratorPermissionsCommandHandler(IAdministratorProfileRepository administratorProfileRepository, IUnitOfWork unitOfWork)
        {
            _administratorProfileRepository = administratorProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateAdministratorPermissionsCommand request, CancellationToken cancellationToken)
        {
            var profile = await _administratorProfileRepository.GetByUserIdAsync(request.userId, cancellationToken);
            if (profile is null)
                return AdministratorStaffProfileErrors.NotFound(request.userId);

            var result = profile.UpdatePermissions(request.permissions);
            if (result.IsFailure)
                return result.Error;

            _administratorProfileRepository.Update(profile);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}