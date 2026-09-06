using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.AdministratorProfile
{
    public static class AdministratorStaffProfileErrors
    {
        public static Error NotFound(Guid userId) => Error.NotFound("AdministratorProfile.NotFound", $"No administrator profile found for user id '{userId}'.");
        public static Error AlreadyExists(Guid userId) => Error.Conflict("AdministratorProfile.AlreadyExists", $"User '{userId}' is already an administrator.");
    }
}
