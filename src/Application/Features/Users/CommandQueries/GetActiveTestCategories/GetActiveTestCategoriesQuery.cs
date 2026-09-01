using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Users.CommandQueries.GetActiveTestCategories
{
    public record GetActiveTestCategoriesQuery() : IRequest<ResultT<List<PublicTestCategoryDto>>>;
}