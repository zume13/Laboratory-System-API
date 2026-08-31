using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.TestCategory;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.TestCategories.ReactivateTestCategory
{
    public class ReactivateTestCategoryCommandHandler : IRequestHandler<ReactivateTestCategoryCommand, Result>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReactivateTestCategoryCommandHandler(
            ITestCategoryRepository testCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ReactivateTestCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId, cancellationToken);
            if (category is null)
                return TestCategoryErrors.NotFound(request.testCategoryId);

            var reactivateResult = category.Reactivate();
            if (reactivateResult.IsFailure)
                return reactivateResult.Error;

            _testCategoryRepository.Update(category);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}