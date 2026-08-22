using Application.Abstractions.Auth;
using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Identity.PatientProfile.Enums;
using Domain.Aggregates.Identity.UserProfile;
using Domain.Aggregates.Identity.UserProfile.Enums;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Shared;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResultT<TokenDto>> LocalLogInAsync(Email email, string passwordHash, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(email.value, cancellationToken);

            if (user is null)
                return ServiceErrors.Auth.InvalidEmailOrPassword;

            var isPasswordValid = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, passwordHash);

            if (isPasswordValid == PasswordVerificationResult.Failed)
                return ServiceErrors.Auth.InvalidEmailOrPassword;
            
            var token = _tokenService.GenerateTokens(user.Id.ToString(), user.Email.value, user.Role.ToString());
            //fix user entity configuration to include role with conversion

            return ResultT<TokenDto>.Success(token);
        }

        public async Task<ResultT<User>> LocalRegisterAsync(
            Name firstName,
            Name lastName,
            Email email,
            PhoneNumber phoneNumber,
            Sex sex,
            string password,
            UserRole role,
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(email.value, cancellationToken);

            if(user is not null)
                return ServiceErrors.Auth.UserAlreadyExists;

            var hashedPassword = _passwordHasher.HashPassword(null!, password);    

            var newUser = User.Create(firstName, lastName, email, phoneNumber, hashedPassword, role);

            if(newUser.IsFailure)
                return newUser.Error;

            await _userRepository.AddAsync(newUser.value);

            return ResultT<User>.Success(newUser.value);
        }
    }
}
