using FluentValidation;

namespace Application.Features.LabOrder.CancelLabOrder
{
    public class CancelLabOrderCommandValidator : AbstractValidator<CancelLabOrderCommand>  
    {
        public CancelLabOrderCommandValidator() 
        {
            RuleFor(x => x.LabOrderId).NotEmpty().WithMessage("Lab Order Id is required.");
        }
    }
}
