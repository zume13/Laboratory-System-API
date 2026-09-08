using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.AdministratorProfile
{
    public class AdministratorProfile : AggregateRoot
    {
        private AdministratorProfile() { }
        private AdministratorProfile(
            Guid id,
            Guid userId)
            : base(id)
        {
            UserId = userId;
        }

        public Guid UserId { get; private set; }

        public static ResultT<AdministratorProfile> Create(Guid userId)
        {
            if (userId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(userId));

            return new AdministratorProfile(Guid.NewGuid(), userId);
        }
    }

}
