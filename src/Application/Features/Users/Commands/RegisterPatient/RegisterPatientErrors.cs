using SharedKernel.Shared;

namespace Application.Features.Users.Commands.RegisterPatient
{
    public static class RegisterPatientErrors
    {
        public static Error UserWithEmailAlreadyExists => Error.Conflict("UserWithEmail.AlreadyExists", "User with the provided email already exists.");
    }
}
