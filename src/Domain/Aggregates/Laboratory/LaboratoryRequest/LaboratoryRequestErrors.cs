
using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory.LaboratoryRequest
{
    public static class LaboratoryRequestErrors
    {
        public static class LaboratoryRequest
        {
            public static Error VoidedRequest => Error.Conflict("Request.Voided", "The laboratory request has been voided.");
            public static Error RequestAlreadyExitsts => Error.Conflict("Result.Exist", "The result already exists for this request");
            public static Error RequestResultAlreadyReleased => Error.Conflict("Result.Released", "The result has already been released for this request");
            public static Error ExistingPatientId => Error.Conflict("Request.ExistingPatientId", "The patient already has a laboratory request with the same patient ID.");
            public static Error ResultAlreadyAttached => Error.Conflict("Result.AlreadyAttached", "A result is already attached to this laboratory request.");
        }
        public static class LaboratoryResult 
        { 
        }
    }
}
