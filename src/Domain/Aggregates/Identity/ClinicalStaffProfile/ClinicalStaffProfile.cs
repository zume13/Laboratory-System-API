using Domain.Aggregates.Identity.User.Enums;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.ClinicalStaffProfile
{
    public class ClinicalStaffProfile : AggregateRoot
    {
        private ClinicalStaffProfile(
            Guid id,
            Guid userId,
            StaffRole role)
            : base(id)
        {
            UserId = userId;
            Role = role;
            IsActive = true;
        }

        public Guid UserId { get; private set; }

        public StaffRole Role { get; private set; }

        public bool IsActive { get; private set; }

        public static ResultT<ClinicalStaffProfile> Create(Guid userId, StaffRole role)
        {
            if (userId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(userId));

            return new ClinicalStaffProfile(Guid.NewGuid(), userId, role);
        }

        public Result ChangeRole(StaffRole newRole)
        {
            if (!IsActive)
                return ClinicalStaffProfileErrors.InactiveEmployee;

            Role = newRole;

            return Result.Success();
        }

        public Result Deactivate()
        {
            if (!IsActive)
                return ClinicalStaffProfileErrors.InactiveEmployee;

            IsActive = false;

            return Result.Success();
        }

        public Result Reactivate()
        {
            if (IsActive)
                return ClinicalStaffProfileErrors.AlreadyActiveEmployee;

            IsActive = true;

            return Result.Success();
        }
    }
}
