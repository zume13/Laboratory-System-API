using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.UserProfile
{
    public static class UserErrors
    {
        public static Error NotFound(Guid userId) => Error.NotFound("User.NotFound", $"No user found with id '{userId}'.");
    }
}
