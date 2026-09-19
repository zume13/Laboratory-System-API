
namespace Application.Abstractions.Auth
{
    public interface IEmailService
    {
        Task VerifyEmailAsync(Guid UserId, string email, string VerificationUrl, CancellationToken token = default);
        Task EmailOtpAsync(Guid UserId, string email, string otp, CancellationToken token = default);
    }
}
