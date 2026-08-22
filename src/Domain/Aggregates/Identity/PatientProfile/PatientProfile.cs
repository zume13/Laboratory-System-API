using Domain.Aggregates.Identity.PatientProfile.Enums;
using SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.PatientProfile
{
    public class PatientProfile : AggregateRoot
    {
        private PatientProfile(
            Guid id,
            Guid userId,
            DateTime dateOfBirth,
            Sex sex,
            bool consent)
            : base(id)
        {
            UserId = userId;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            ConsentAccepted = consent;
        }

        public Guid UserId { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public Sex Sex { get; private set; }

        public string? PhysicalPatientId { get; private set; }

        public bool ConsentAccepted { get; private set; }

        public static ResultT<PatientProfile> Create(
            Guid userId,
            DateTime dateOfBirth,
            Sex sex,
            bool consent)
        {
            if (userId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(userId));

            return new PatientProfile(
                Guid.NewGuid(),
                userId,
                dateOfBirth,
                sex,
                consent);
        }

        public Result AcceptConsent()
        {
            if (ConsentAccepted)
                return PatientProfileErrors.ConsentAlreadyAccepted;

            ConsentAccepted = true;

            return Result.Success();
        }

        public Result LinkPhysicalRecord(string physicalPatientId)
        {
            if (string.IsNullOrWhiteSpace(physicalPatientId))
                return GeneralErrors.General.Empty(nameof(physicalPatientId));

            if (PhysicalPatientId is not null)
                return PatientProfileErrors.PhysicalPatientIdAlreadyLinked;

            PhysicalPatientId = physicalPatientId;

            return Result.Success();
        }
    }

}
