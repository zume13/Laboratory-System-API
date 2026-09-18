using FluentValidation;

namespace Application.Features.StorageStatus.Command.CreateStorageStatusRecord
{
    public class CreateStorageStatusRecordCommandValidator : AbstractValidator<CreateStorageStatusRecordCommand>
    {
        public CreateStorageStatusRecordCommandValidator()
        {
            RuleFor(x => x.storageType).NotEmpty().WithMessage("Storage type is required.");
            RuleFor(x => x.capacityGb).GreaterThan(0).WithMessage("Capacity must be greater than 0.");
        }
    }
}