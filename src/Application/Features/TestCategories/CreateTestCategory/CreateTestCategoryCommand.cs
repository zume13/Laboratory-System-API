using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.CreateTestCategory
{
    public record CreateTestCategoryCommand(
        string name, decimal price
        ) : IRequest<Result>;
}