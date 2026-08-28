using FluentValidation;


namespace Application.TestCategories.UpdateTestCategoryPrice
{
    public class UpdateTestCategoryPriceCommandValidator : AbstractValidator<UpdateTestCategoryPriceCommand>
    {
        public UpdateTestCategoryPriceCommandValidator()
        {
            RuleFor(k => k.testCategoryId).NotEmpty().WithMessage("Test Category ID is required.");
            RuleFor(a => a.price).GreaterThanOrEqualTo(0).WithMessage("New Test Price should not be negative.");
        }
    }
}