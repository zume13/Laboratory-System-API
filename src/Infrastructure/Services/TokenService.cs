using Application.Abstractions.Auth;
using Application.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenDto GenerateTokens(string userId, string email, string role)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var accessTokenExpiration = GetAccessTokenExpiration(int.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"]!));
            var refreshTokenExpiration = GetRefreshTokenExpiration(int.Parse(_configuration["Jwt:RefreshTokenExpirationDays"]!));

            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, email),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: accessTokenExpiration,
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.WriteToken(token);

            var refreshToken = GenerateRefreshToken(userId, email, role);

            return new TokenDto(jwtToken, refreshToken, accessTokenExpiration, refreshTokenExpiration);
        }

        private string GenerateRefreshToken(string userId, string email, string role)
        {
            var randomNumber = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomNumber);
        }

        private DateTime GetRefreshTokenExpiration(int tokenLifetimeDays)
        {
            return DateTime.UtcNow.AddDays(tokenLifetimeDays);
        }

        private DateTime GetAccessTokenExpiration(int expirationMinutes)
        {
            return DateTime.UtcNow.AddMinutes(expirationMinutes);
        }
    }
}
