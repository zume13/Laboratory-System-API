
namespace Application.Abstractions.Auth
{
    public interface IEmailService
    {
        Task VerifyEmailAsync(Guid UserId, string email, string VerificationUrl);
        Task 
    }
}
