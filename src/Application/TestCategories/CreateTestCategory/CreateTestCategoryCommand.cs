using MediatR;
using SharedKernel.Shared;

namespace Application.TestCategories.CreateTestCategory
{
    public record CreateTestCategoryCommand(
        string name, decimal price
        ) : IRequest<Result>;
}