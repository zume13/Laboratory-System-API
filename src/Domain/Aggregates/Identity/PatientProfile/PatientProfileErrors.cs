using SharedKernel.Shared;

namespace Domain.Aggregates.Identity.PatientProfile
{
    public static class PatientProfileErrors
    {
        public static Error ConsentAlreadyAccepted => Error.Conflict("Consent.AlreadyAccepted", "Consent has already been accepted");
        public static Error PhysicalPatientIdAlreadyLinked => Error.Conflict("PhysicalPatientId.AlreadyLinked", "A physical patient ID has already been linked");
    }
}
