using Application.Abstractions.Auth;
using Microsoft.Extensions.Configuration;
using Resend;

namespace Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    private readonly IResend _resend;
    private readonly string _fromEmail;

    public EmailService(
        IResend resend,
        IConfiguration configuration)
    {
        _resend = resend;

        _fromEmail = configuration["Resend:From"]
            ?? throw new InvalidOperationException(
                "Resend:From is not configured.");
    }

    public async Task VerifyEmailAsync(
        Guid userId,
        string email,
        string verificationUrl,
        CancellationToken cancellationToken = default)
    {
        var message = new EmailMessage
        {
            From = _fromEmail,
            Subject = "Verify your email address",
            HtmlBody = $"""
                <html>
                    <body>
                        <h2>Verify your email address</h2>

                        <p>
                            Thank you for creating an account.
                        </p>

                        <p>
                            Please click the button below to verify
                            your email address.
                        </p>

                        <p>
                            <a href="{verificationUrl}">
                                Verify Email
                            </a>
                        </p>

                        <p>
                            If you did not create this account,
                            you can safely ignore this email.
                        </p>
                    </body>
                </html>
                """
        };

        message.To.Add(email);

        await _resend.EmailSendAsync(message);
    }

    public async Task EmailOtpAsync(
        Guid userId,
        string email,
        string otp,
        CancellationToken cancellationToken = default)
    {
        var message = new EmailMessage
        {
            From = _fromEmail,
            Subject = "Your verification code",
            HtmlBody = $"""
                <html>
                    <body>
                        <h2>Email Verification</h2>

                        <p>
                            Your verification code is:
                        </p>

                        <h1>{otp}</h1>

                        <p>
                            This code will expire shortly.
                        </p>

                        <p>
                            If you did not request this code,
                            you can safely ignore this email.
                        </p>
                    </body>
                </html>
                """
        };

        message.To.Add(email);

        await _resend.EmailSendAsync(message);
    }
}