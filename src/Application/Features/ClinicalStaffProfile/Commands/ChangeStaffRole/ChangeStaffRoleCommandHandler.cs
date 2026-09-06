using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.ClinicalStaffProfile;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.ClinicalStaffProfile.Commands.ChangeStaffRole
{
    public class ChangeStaffRoleCommandHandler : IRequestHandler<ChangeStaffRoleCommand, Result>
    {
        private readonly IClinicalProfileRepository _clinicalProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeStaffRoleCommandHandler(IClinicalProfileRepository clinicalProfileRepository, IUnitOfWork unitOfWork)
        {
            _clinicalProfileRepository = clinicalProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ChangeStaffRoleCommand request, CancellationToken cancellationToken)
        {
            var profile = await _clinicalProfileRepository.GetByUserIdAsync(request.staffUserId, cancellationToken);
            if (profile is null)
                return ClinicalStaffProfileErrors.NotFound(request.staffUserId);

            var result = profile.ChangeRole(request.newRole);
            if (result.IsFailure)
                return result.Error;

            _clinicalProfileRepository.Update(profile);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}