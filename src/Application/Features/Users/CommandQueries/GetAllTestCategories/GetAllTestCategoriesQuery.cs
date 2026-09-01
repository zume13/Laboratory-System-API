using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Users.CommandQueries.GetAllTestCategories
{
    public record GetAllTestCategoriesQuery() : IRequest<ResultT<List<TestCategoryDto>>>;
}