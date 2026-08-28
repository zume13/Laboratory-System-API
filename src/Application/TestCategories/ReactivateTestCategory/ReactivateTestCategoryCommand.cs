using MediatR;
using SharedKernel.Shared;

namespace Application.TestCategories.ReactivateTestCategory
{
    public record ReactivateTestCategoryCommand(Guid testCategoryId) : IRequest<Result>;
}