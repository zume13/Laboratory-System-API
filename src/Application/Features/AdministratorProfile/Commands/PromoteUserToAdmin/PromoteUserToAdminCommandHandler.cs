using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.AdministratorProfile;
using adminProfile = Domain.Aggregates.Identity.AdministratorProfile.AdministratorProfile;
using Domain.Aggregates.Identity.UserProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.AdministratorProfile.Commands.PromoteUserToAdmin
{
    public class PromoteUserToAdminCommandHandler : IRequestHandler<PromoteUserToAdminCommand, ResultT<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdministratorProfileRepository _administratorProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PromoteUserToAdminCommandHandler(
            IUserRepository userRepository,
            IAdministratorProfileRepository administratorProfileRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _administratorProfileRepository = administratorProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultT<Guid>> Handle(PromoteUserToAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);
            if (user is null)
                return UserErrors.NotFound(request.userId);

            var existingProfile = await _administratorProfileRepository.GetByUserIdAsync(request.userId, cancellationToken);
            if (existingProfile is not null)
                return AdministratorStaffProfileErrors.AlreadyExists(request.userId);

            var assignResult = user.AssignToRole(UserRole.Admin);
            if (assignResult.IsFailure)
                return assignResult.Error;

            var profileResult = adminProfile.Create(request.userId);
            if (profileResult.IsFailure)
                return profileResult.Error;

            _userRepository.Update(user);
            await _administratorProfileRepository.AddAsync(profileResult.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return profileResult.value.Id;
        }
    }
}