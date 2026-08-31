using SharedKernel.Shared;

namespace Application.Features.Users.RegisterEmployee
{
    public static class RegisterEmployeeErrors
    {
        public static Error UserWithEmailAlreadyExists => Error.Conflict("UserWithEmail.AlreadyExists", "User with the provided email already exists.");
    }
}
