using FluentValidation;

namespace Application.Features.LaboratoryRequests.Commands.VoidLaboratoryRequest
{
    public class VoidLaboratoryRequestCommandValidator : AbstractValidator<VoidLaboratoryRequestCommand>
    {
        public VoidLaboratoryRequestCommandValidator()
        {
            RuleFor(x => x.laboratoryRequestId).NotEmpty().WithMessage("Laboratory request id is required.");
        }
    }
}