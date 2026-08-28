using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.TestCategory;
using MediatR;
using SharedKernel.Shared;

namespace Application.TestCategories.UpdateTestCategoryPrice
{
    public class UpdateTestCategoryPriceCommandHandler : IRequestHandler<UpdateTestCategoryPriceCommand, Result>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTestCategoryPriceCommandHandler(
            ITestCategoryRepository testCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTestCategoryPriceCommand request, CancellationToken cancellationToken)
        {
            var category = await _testCategoryRepository.GetByIdAsync(request.testCategoryId, cancellationToken);
            if (category is null)
                return TestCategoryErrors.NotFound(request.testCategoryId);

            var updateResult = category.UpdatePrice(request.price);
            if (updateResult.IsFailure)
                return updateResult.Error;

            _testCategoryRepository.Update(category);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}