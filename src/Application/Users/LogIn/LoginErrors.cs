using SharedKernel.Shared;

namespace Application.Users.LogIn
{
    public static class LoginErrors
    {
        public static Error UserNotWithEmailNotFound(string email) => Error.Failure("User.NotFound", $"User with email '{email}' not found.");
    }
}
