using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.Commands.ReactivateTestCategory
{
    public record ReactivateTestCategoryCommand(Guid testCategoryId) : IRequest<Result>;
}