using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.Queries.GetAllTestCategories
{
    public class GetAllTestCategoriesQueryHandler : IRequestHandler<GetAllTestCategoriesQuery, ResultT<List<TestCategoryDto>>>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;
        public GetAllTestCategoriesQueryHandler(ITestCategoryRepository testCategoryRepository)
        {
            _testCategoryRepository = testCategoryRepository;
        }
        public async Task<ResultT<List<TestCategoryDto>>> Handle(
           GetAllTestCategoriesQuery request,
           CancellationToken cancellationToken)
        {
            var categories = await _testCategoryRepository.GetAllAsync(cancellationToken);

            var dtos = categories
                .Select(c => new TestCategoryDto(c.Id, c.Name.value, c.Price.value, c.IsActive))
                .ToList();
            return dtos;
        }
    }
}
