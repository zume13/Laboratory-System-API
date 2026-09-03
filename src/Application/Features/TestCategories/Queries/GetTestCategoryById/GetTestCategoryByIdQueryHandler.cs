using Application.Abstractions.Repositories;
using Application.Dto;
using Domain.Aggregates.Laboratory.TestCategory;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.GetTestCategoryById
{
    public class GetTestCategoryByIdQueryHandler : IRequestHandler<GetTestCategoryByIdQuery, ResultT<TestCategoryDto>>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;

        public GetTestCategoryByIdQueryHandler(ITestCategoryRepository testCategoryRepository)
        {
            _testCategoryRepository = testCategoryRepository;
        }

        public async Task<ResultT<TestCategoryDto>> Handle(GetTestCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId, cancellationToken);
            if (category is null)
                return TestCategoryErrors.NotFound(request.testCategoryId);

            return new TestCategoryDto(category.Id, category.Name.value, category.Price.value, category.IsActive);
        }
    }
}