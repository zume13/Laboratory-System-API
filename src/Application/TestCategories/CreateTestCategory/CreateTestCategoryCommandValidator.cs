using FluentValidation;

namespace Application.TestCategories.CreateTestCategory
{
    public class CreateTestCategoryCommandValidator : AbstractValidator<CreateTestCategoryCommand>
    {
        public CreateTestCategoryCommandValidator()
        {
            RuleFor(c => c.name).NotEmpty().WithMessage("Test Category is required.").MaximumLength(100).WithMessage("Test Category Name must not exceed 100 characters.");
            RuleFor(h => h.price).GreaterThanOrEqualTo(0).WithMessage("Test Price shan't be negative.");
        }
    }
}
