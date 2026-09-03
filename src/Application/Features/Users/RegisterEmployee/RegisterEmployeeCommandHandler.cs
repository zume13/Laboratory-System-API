using Application.Abstractions.Auth;
using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.UserProfile.Enums;
using ClinicalStaffProfileEntity = Domain.Aggregates.Identity.ClinicalStaffProfile.ClinicalStaffProfile;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Users.RegisterEmployee
{
    public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, Result>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IClinicalProfileRepository _clinicalProfileRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterEmployeeCommandHandler(IUserRepository userRepository, IAuthService service, IClinicalProfileRepository clinicalProfileRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _authService = service;
            _clinicalProfileRepository = clinicalProfileRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var firstNameResult = Name.Create(request.firstName);
            if (firstNameResult.IsFailure)
                return firstNameResult.Error;

            var lastNameResult = Name.Create(request.lastName);
            if (lastNameResult.IsFailure)
                return lastNameResult.Error;

            var emailResult = Email.Create(request.email);
            if (emailResult.IsFailure)
                return emailResult.Error;

            var phoneResult = PhoneNumber.Create(request.phoneNumber);
            if (phoneResult.IsFailure)
                return phoneResult.Error;

            var newUser = await _authService.LocalRegisterAsync(
                firstNameResult.value,
                lastNameResult.value,
                emailResult.value,
                phoneResult.value,
                request.password,
                UserRole.ClinicalStaff);

            if (newUser.IsFailure)
                return newUser.Error;

            var profile = ClinicalStaffProfileEntity.Create(newUser.value.Id, request.staffRole);

            if (profile.IsFailure)
                return profile.Error;

            await _clinicalProfileRepository.AddAsync(profile.value, cancellationToken);
            await _userRepository.AddAsync(newUser.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}
