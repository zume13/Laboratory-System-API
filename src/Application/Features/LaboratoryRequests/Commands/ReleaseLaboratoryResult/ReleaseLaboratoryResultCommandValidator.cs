using FluentValidation;

namespace Application.Features.LaboratoryRequests.Commands.ReleaseLaboratoryResult
{
    public class ReleaseLaboratoryResultCommandValidator : AbstractValidator<ReleaseLaboratoryResultCommand>
    {
        public ReleaseLaboratoryResultCommandValidator()
        {
            RuleFor(p => p.laboratoryRequestId).NotEmpty().WithMessage("Laboratory request id is required.");
        }
    }
}