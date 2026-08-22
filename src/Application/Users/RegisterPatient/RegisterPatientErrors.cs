using SharedKernel.Shared;

namespace Application.Users.RegisterPatient
{
    public static class RegisterPatientErrors
    {
        public static Error UserWithEmailAlreadyExists => Error.Conflict("UserWithEmail.AlreadyExists", "User with the provided email already exists.");
    }
}
