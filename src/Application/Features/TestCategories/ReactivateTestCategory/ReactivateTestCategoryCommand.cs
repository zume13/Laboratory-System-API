using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.ReactivateTestCategory
{
    public record ReactivateTestCategoryCommand(Guid testCategoryId) : IRequest<Result>;
}