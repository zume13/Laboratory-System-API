using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.Queries.GetAllTestCategories
{
    public record GetAllTestCategoriesQuery() : IRequest<ResultT<List<TestCategoryDto>>>;
}