
using Application.Abstractions.Auth;
using Application.Dto;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public TokenDto GenerateTokens(string userId, string email, string role)
        {
            throw new NotImplementedException();
        }
    }
}
