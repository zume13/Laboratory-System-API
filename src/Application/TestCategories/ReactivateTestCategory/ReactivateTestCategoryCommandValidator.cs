using FluentValidation;

namespace Application.TestCategories.ReactivateTestCategory
{
    public class ReactivateTestCategoryCommandValidator : AbstractValidator<ReactivateTestCategoryCommand>
    {
        public ReactivateTestCategoryCommandValidator()
        {
            RuleFor(s => s.testCategoryId)
                .NotEmpty().WithMessage("Test category id is required.");
        }
    }
}