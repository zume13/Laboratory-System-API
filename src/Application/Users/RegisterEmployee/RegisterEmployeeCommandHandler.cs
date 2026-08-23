using Application.Abstractions.Auth;
using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.ClinicalStaffProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Shared;

namespace Application.Users.RegisterEmployee
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
            var newUser = await _authService.LocalRegisterAsync(
                Name.Create(request.firstName).value, 
                Name.Create(request.lastName).value, 
                Email.Create(request.email).value, 
                PhoneNumber.Create(request.phoneNumber).value, 
                request.password, 
                UserRole.ClinicalStaff);

            if (newUser.IsFailure)
                return newUser.Error;

            var profile = ClinicalStaffProfile.Create(newUser.value.Id, request.staffRole);

            if(profile.IsFailure)
                return profile.Error;

            await _clinicalProfileRepository.AddAsync(profile.value, cancellationToken);
            await _userRepository.AddAsync(newUser.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if(saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}
