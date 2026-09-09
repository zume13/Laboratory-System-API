using SharedKernel.Shared;

namespace Application.Features.Users.Commands.LogIn
{
    public static class LoginErrors
    {
        public static Error UserNotWithEmailNotFound(string email) => Error.Failure("User.NotFound", $"User with email '{email}' not found.");
    }
}
