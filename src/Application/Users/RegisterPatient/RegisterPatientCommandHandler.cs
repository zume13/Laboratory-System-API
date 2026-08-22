using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Identity.PatientProfile;
using Domain.Aggregates.Identity.UserProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Shared;

namespace Application.Users.RegisterPatient
{
    public class RegisterPatientCommandHandler : IRequestHandler<RegisterPatientCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IPatientProfileRepository _patientProfileRepository;

        public RegisterPatientCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher<User> hasher, IPatientProfileRepository patientProfileRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _hasher = hasher;
            _patientProfileRepository = patientProfileRepository;
        }

        public async Task<Result> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userRepository.ExistsByEmailAsync(request.email, cancellationToken);
            
            if (emailExists)
                return RegisterPatientErrors.UserWithEmailAlreadyExists;

            var user = User.Create(
                Name.Create(request.firstName).value,
                Name.Create(request.lastName).value,
                Email.Create(request.email).value,
                PhoneNumber.Create(request.phoneNumber).value,
                _hasher.HashPassword(null!, request.password),
                UserRole.Patient
            );

            if (user.IsFailure)
                return user.Error;

            var patienProfile = PatientProfile.Create(user.value.Id, request.DateOfBirth, request.sex, request.consent);

            await _userRepository.AddAsync(user.value, cancellationToken);
            await _patientProfileRepository.AddAsync(patienProfile.value, cancellationToken);   

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}
