using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.Queries.GetActiveTestCategories
{
    public record GetActiveTestCategoriesQuery() : IRequest<ResultT<List<PublicTestCategoryDto>>>;
}