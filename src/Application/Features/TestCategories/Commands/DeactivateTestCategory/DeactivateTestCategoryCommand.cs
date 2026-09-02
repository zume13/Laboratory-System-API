using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.Commands.DeactivateTestCategory
{
    public record DeactivateTestCategoryCommand(Guid testCategoryId) : IRequest<Result>;
}