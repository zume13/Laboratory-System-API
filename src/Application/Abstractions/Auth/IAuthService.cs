using Application.Dto;
using Domain.Aggregates.Identity.UserProfile.Enums;
using Domain.ValueObjects;
using SharedKernel.Shared;
using Domain.Aggregates.Identity.UserProfile;

namespace Application.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<ResultT<TokenDto>> LocalLogInAsync(Email email, string password, CancellationToken cancellationToken = default);

        Task<ResultT<User>> LocalRegisterAsync(
            Name firstName,
            Name lastName,
            Email email,
            PhoneNumber phoneNumber,
            string password, 
            UserRole role,
            CancellationToken cancellationToken = default);
    }
}
