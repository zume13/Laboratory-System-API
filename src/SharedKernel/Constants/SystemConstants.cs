
namespace SharedKernel.Constants
{
    public static class SystemConstants
    {
        public static class Roles 
        {
            public const string ClinicalStaff = "ClinicalStaff";
            public const string Patient = "Patient";
            public const string Admin = "Admin";
        }

        public static class AuthPolicies 
        {
            public const string adminOnly = "admin-only";
            public const string companyPersonnel = "company-personnel";
        }


        public static class RateLimits
        {
            public const string perUser = "per-user";
            public const string anonymous = "anonymous";
            public const string unknown = "unknown";
        }

    }
}
