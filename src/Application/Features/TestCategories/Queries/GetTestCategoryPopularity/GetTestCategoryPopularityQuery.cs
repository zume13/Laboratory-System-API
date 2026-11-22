using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.Queries.GetTestCategoryPopularity
{
    public record GetTestCategoryPopularityQuery() : IRequest<ResultT<List<TestCategoryPopularityDto>>>;
}