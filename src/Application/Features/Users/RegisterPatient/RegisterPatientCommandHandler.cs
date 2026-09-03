using Application.Abstractions.Auth;
using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using PatientProfileEntity = Domain.Aggregates.Identity.PatientProfile.PatientProfile;
using Domain.Aggregates.Identity.UserProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Shared;

namespace Application.Features.Users.RegisterPatient
{
    public class RegisterPatientCommandHandler : IRequestHandler<RegisterPatientCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IPatientProfileRepository _patientProfileRepository;

        public RegisterPatientCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAuthService service, IPatientProfileRepository patientProfileRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _authService = service;
            _patientProfileRepository = patientProfileRepository;
        }

        public async Task<Result> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
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

            var patienProfile = PatientProfileEntity.Create(newUser.value.Id, request.DateOfBirth, request.sex, request.consent);

            if (patienProfile.IsFailure)
                return patienProfile.Error;

            await _userRepository.AddAsync(newUser.value, cancellationToken);
            await _patientProfileRepository.AddAsync(patienProfile.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}
