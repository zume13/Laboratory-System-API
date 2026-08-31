using MediatR;
using SharedKernel.Shared;
namespace Application.TestCategories.UpdateTestCategoryPrice
{
    public record UpdateTestCategoryPriceCommand(
        Guid testCategoryId, decimal price
        ) : IRequest<Result>;
}