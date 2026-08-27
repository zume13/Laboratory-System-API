using Application.Abstractions.Base;
using Application.Abstractions.Repositories;
using Domain.Aggregates.Laboratory.TestCategory;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Shared;

namespace Application.TestCategories.CreateTestCategory
{
    public class CreateTestCategoryCommandHandler : IRequestHandler<CreateTestCategoryCommand, Result>
    {
        private readonly ITestCategoryRepository _testCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTestCategoryCommandHandler(ITestCategoryRepository testCategoryRepository, IUnitOfWork unitOfWork)
        {
            _testCategoryRepository = testCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateTestCategoryCommand request, CancellationToken cancellationToken)
        {
            var existing = await _testCategoryRepository.GetByNameAsync(request.name, cancellationToken);
            if (existing is not null)
                return TestCategoryErrors.AlreadyExists(request.name);

            var nameResult = Name.Create(request.name);
            if (nameResult.IsFailure)
                return nameResult.Error;

            var priceResult = Money.Create(request.price);
            if (priceResult.IsFailure)
                return priceResult.Error;

            var categoryResult = TestCategory.Create(nameResult.value, priceResult.value);
            if (categoryResult.IsFailure)
                return categoryResult.Error;

            await _testCategoryRepository.AddAsync(categoryResult.value, cancellationToken);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return saveResult.Error;

            return Result.Success();
        }
    }
}