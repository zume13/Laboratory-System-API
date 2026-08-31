using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.DeactivateTestCategory
{
    public record DeactivateTestCategoryCommand(Guid testCategoryId) : IRequest<Result>;
}