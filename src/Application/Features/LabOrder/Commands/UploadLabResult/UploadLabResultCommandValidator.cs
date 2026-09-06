using Application.Features.LabOrder.Commands.UploadLaboratoryResult;
using FluentValidation;

namespace Application.Features.LabOrder.Commands.UploadLabResult
{
    internal class UploadLabResultCommandValidator : AbstractValidator<UploadLabResultCommand>
    {
        public UploadLabResultCommandValidator()
        {
            RuleFor(x => x.labOrderId).NotEmpty().WithMessage("Lab Order Id is required.");
            RuleFor(x => x.requestId).NotEmpty().WithMessage("Request Id is required.");
            RuleFor(x => x.uploadedByStaffId).NotEmpty().WithMessage("Uploaded By Staff Id is required.");
            RuleFor(x => x.fileName).NotEmpty().WithMessage("File name is required.");
            RuleFor(x => x.sampleId).NotEmpty().WithMessage("Sample Id is required.");
            RuleFor(x => x.fileStream).NotNull().WithMessage("File stream is required");
            RuleFor(x => x.subFolder).NotEmpty().WithMessage("Sub folder is required");
        }   
    }
}
