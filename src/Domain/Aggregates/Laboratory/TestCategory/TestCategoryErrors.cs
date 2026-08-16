using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory.TestCategory
{
    public static class TestCategoryErrors
    {
        public static Error InactiveCategory(string CategoryName) => Error.Conflict("TestCategory.Inactive", $"The test category '{CategoryName}' is inactive");
        public static Error AlreadyActiveCategory(string CategoryName) => Error.Conflict("TestCategory.Active", $"The test category '{CategoryName}' is already active");
    }
}
