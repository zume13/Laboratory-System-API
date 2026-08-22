

using SharedKernel.Shared;

namespace Infrastructure.Services
{
    public static class ServiceErrors
    {
        public static class Auth
        {
            public static Error InvalidEmailOrPassword => Error.Validation("Credentials.Invalid", "Invalid email or password.");
            public static Error UserAlreadyExists => Error.Validation("User.Exists", "A user with the provided email already exists.");
            public static Error InvalidRefreshToken => Error.Validation("RefreshToken.Invalid", "The provided refresh token is invalid.");
        }
    }
}
