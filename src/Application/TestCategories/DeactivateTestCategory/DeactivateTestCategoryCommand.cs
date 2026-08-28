using MediatR;
using SharedKernel.Shared;

namespace Application.TestCategories.DeactivateTestCategory
{
    public record DeactivateTestCategoryCommand(Guid testCategoryId) : IRequest<Result>;
}