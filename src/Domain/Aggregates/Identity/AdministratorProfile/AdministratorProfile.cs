using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.AdministratorProfile
{
    public class AdministratorProfile : AggregateRoot
    {
        private AdministratorProfile(
            Guid id,
            Guid userId,
            string permissions)
            : base(id)
        {
            UserId = userId;
            Permissions = permissions;
        }

        public Guid UserId { get; private set; }

        public string Permissions { get; private set; }

        public static ResultT<AdministratorProfile> Create(Guid userId, string permissions)
        {
            if (userId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(userId));

            return new AdministratorProfile(Guid.NewGuid(), userId, permissions ?? string.Empty);
        }

        public Result UpdatePermissions(string permissions)
        {
            Permissions = permissions ?? string.Empty;

            return Result.Success();
        }
    }

}
