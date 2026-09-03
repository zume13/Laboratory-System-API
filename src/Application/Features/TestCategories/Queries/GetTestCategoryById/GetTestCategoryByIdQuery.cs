using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.GetTestCategoryById
{
    public record GetTestCategoryByIdQuery(Guid testCategoryId) : IRequest<ResultT<TestCategoryDto>>;
}