using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.Queries.GetActiveTestCategories
{
    public class GetActiveTestCategoriesQueryHandler : IRequestHandler<GetActiveTestCategoriesQuery, ResultT<List<PublicTestCategoryDto>>>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;
        public GetActiveTestCategoriesQueryHandler(ITestCategoryRepository testCategoryRepository)
        {
            _testCategoryRepository = testCategoryRepository;
        }

        public async Task<ResultT<List<PublicTestCategoryDto>>> Handle(
           GetActiveTestCategoriesQuery request,
           CancellationToken cancellationToken)
        {
            var categories = await _testCategoryRepository.GetAllAsync(cancellationToken);

            var dtos = categories
                .Select(c => new PublicTestCategoryDto(c.Id, c.Name.value, c.Price.value))
                .ToList();
            return dtos;
        }
    }
}
