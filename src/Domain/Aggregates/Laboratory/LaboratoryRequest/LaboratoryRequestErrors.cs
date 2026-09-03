
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
            public static Error NotFound(Guid id) => Error.NotFound("Request.NotFound", $"No laboratory request found with id '{id}'.");
        }
        public static class LaboratoryResult 
        { 
            public static Error NotFound(Guid id) => Error.NotFound("Result.NotFound", $"No laboratory result found with id '{id}'.");
            public static Error ResultAlreadyReleased => Error.Conflict("Result.AlreadyReleased", "The result has already been released.");
            public static Error ResultAlreadyVoided => Error.Conflict("Result.AlreadyVoided", "The result has already been voided.");
            public static Error InvalidFileType => Error.Conflict("Result.InvalidFileType", "The file type is not allowed. Only PDF files are accepted.");
        }
    }
}
