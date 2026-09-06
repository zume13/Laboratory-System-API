using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.ClinicalStaffProfile
{
    public static class ClinicalStaffProfileErrors
    {
        public static Error InactiveEmployee => Error.Conflict("Employee.Inactive", "This employee is inactive");
        public static Error AlreadyActiveEmployee => Error.Conflict("Employee.AlreadyActive", "This employee is inactive");

        public static Error NotFound(Guid userId) => Error.NotFound("ClinicalStaffProfile.NotFound", $"No clinical staff profile found for user id: '{userId}'.");
    }
}
