using FluentValidation;

namespace Application.Features.StorageStatus.Command.UpdateStorageUsage
{
    public class UpdateStorageUsageCommandValidator : AbstractValidator<UpdateStorageUsageCommand>
    {
        public UpdateStorageUsageCommandValidator()
        {
            RuleFor(x => x.storageType).NotEmpty().WithMessage("Storage type is required.");
            RuleFor(x => x.usedGb).GreaterThanOrEqualTo(0).WithMessage("Used space cannot be negative.");
        }
    }
}