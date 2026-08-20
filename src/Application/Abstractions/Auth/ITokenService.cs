
using Application.Dto;

namespace Application.Abstractions.Auth
{
    public interface ITokenService
    {
        TokenDto GenerateTokens(string userId, string email, string role);
    }
}
