using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.PatientProfile
{
    public static class PatientProfileErrors
    {
        public static Error ConsentAlreadyAccepted => Error.Conflict("Consent.AlreadyAccepted", "Consent has already been accepted");
        public static Error PhysicalPatientIdAlreadyLinked => Error.Conflict("PhysicalPatientId.AlreadyLinked", "A physical patient ID has already been linked");
        public static Error DateOfBirthInvalid => Error.Conflict("DateOfBirth.Invalid", "The provided date of birth was invalid");

        public static Error NotFound(Guid userId) => Error.NotFound("PatientProfile.NotFound", $"No patient profile found for user id '{userId}'.");
    }
}
