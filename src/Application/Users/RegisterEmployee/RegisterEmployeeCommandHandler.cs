using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.ClinicalStaffProfile;
using Domain.Aggregates.Identity.UserProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Shared;

namespace Application.Users.RegisterEmployee
{
    public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClinicalProfileRepository _profileRepository; 

        public RegisterEmployeeCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, IClinicalProfileRepository profileRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _profileRepository = profileRepository;
        }
        public async Task<Result> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userRepository.ExistsByEmailAsync(request.email, cancellationToken);

            if(emailExists)
                return RegisterEmployeeErrors.UserWithEmailAlreadyExists;

            var hashedPassword = _passwordHasher.HashPassword(null!, request.password);

            var user = User.Create(
                Name.Create(request.firstName).value, 
                Name.Create(request.lastName).value, 
                Email.Create(request.email).value, 
                PhoneNumber.Create(request.phoneNumber).value,
                hashedPassword,
                UserRole.ClinicalStaff);

            if(user.IsFailure)
                return user.Error;

            var profile = ClinicalStaffProfile.Create(user.value.Id, request.staffRole);

            if(profile.IsFailure)
                return profile.Error;

            await _profileRepository.AddAsync(profile.value, cancellationToken);
            await _userRepository.AddAsync(user.value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
