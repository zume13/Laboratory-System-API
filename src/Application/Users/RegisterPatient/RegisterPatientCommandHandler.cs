using Application.Abstractions.Auth;
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
            var newUser = await _authService.LocalRegisterAsync(
                 Name.Create(request.firstName).value,
                 Name.Create(request.lastName).value,
                 Email.Create(request.email).value,
                 PhoneNumber.Create(request.phoneNumber).value,
                 request.password,
                 UserRole.ClinicalStaff);

            if (newUser.IsFailure)
                return newUser.Error;

            var patienProfile = PatientProfile.Create(newUser.value.Id, request.DateOfBirth, request.sex, request.consent);

            await _userRepository.AddAsync(newUser.value, cancellationToken);
            await _patientProfileRepository.AddAsync(patienProfile.value, cancellationToken);   

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}
