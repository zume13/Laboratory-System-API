using Application.Abstractions.Repositories;
using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.Queries.GetTestCategoryPopularity
{
    public class GetTestCategoryPopularityQueryHandler : IRequestHandler<GetTestCategoryPopularityQuery, ResultT<List<TestCategoryPopularityDto>>>
    {
        private readonly ILabOrderRepository _labOrderRepository;

        public GetTestCategoryPopularityQueryHandler(ILabOrderRepository labOrderRepository)
        {
            _labOrderRepository = labOrderRepository;
        }

        public async Task<ResultT<List<TestCategoryPopularityDto>>> Handle(GetTestCategoryPopularityQuery request, CancellationToken cancellationToken)
        {
            var popularity = await _labOrderRepository.GetTestCategoryPopularityAsync(cancellationToken);
            return popularity;
        }
    }
}