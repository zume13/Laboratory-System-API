
namespace Application.Dto
{
    public record TokenDto(
        string AccessToken,
        string RefreshToken,
        DateTime AccessTokenExpiresAt,
        DateTime RefreshTokenExpiresAt
    );
}
