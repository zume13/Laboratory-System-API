using SharedKernel.Shared;

namespace Domain.Aggregates.Laboratory.LaboratoryOrder
{
    public static class LaboratoryOrderErrors
    {

        public static class LabOrder
        {
            public static Error NotFound => Error.Conflict("LabOrder.NotFound", "Lab Order not found.");
            public static Error AlreadyCancelled =>  Error.Conflict("LabOrder.AlreadyCancelled", "Lab Order is already cancelled.");
        }


        public static class Request
        {
            public static Error VoidedRequest => Error.Conflict("Request.Voided", "The laboratory request has been voided.");
            public static Error RequestAlreadyExitsts => Error.Conflict("Result.Exist", "The result already exists for this request");
            public static Error RequestResultAlreadyReleased => Error.Conflict("Result.Released", "The result has already been released for this request");
            public static Error ExistingPatientId => Error.Conflict("Request.ExistingPatientId", "The patient already has a laboratory request with the same patient ID.");
            public static Error ResultAlreadyAttached => Error.Conflict("Result.AlreadyAttached", "A result is already attached to this laboratory request.");
            public static Error NotFound(Guid id) => Error.NotFound("Request.NotFound", $"No laboratory request found with id '{id}'.");
            public static Error NoRequestsProvided => Error.Conflict("Request.NoRequestsProvided", "No laboratory requests were provided.");
            public static Error InvalidStatus => Error.Conflict("Request.InvalidStatus", "The laboratory request has an invalid status.");
            public static Error RequestsStillPending => Error.Conflict("Request.RequestsStillPending", "There are still pending laboratory requests that need to be completed.");
            public static Error DuplicateRequest => Error.Conflict("Request.DuplicateRequest", "A duplicate laboratory request already exists for this patient.");
        }
        public static class LaboratoryResult 
        { 
            public static Error NotFound(Guid id) => Error.NotFound("Result.NotFound", $"No laboratory result found with id '{id}'.");
            public static Error ResultAlreadyReleased => Error.Conflict("Result.AlreadyReleased", "The result has already been released.");
            public static Error ResultAlreadyVoided => Error.Conflict("Result.AlreadyVoided", "The result has already been voided.");
            public static Error InvalidFileType => Error.Conflict("Result.InvalidFileType", "The file type is not allowed. Only PDF files are accepted.");
            public static Error InvalidFilePath => Error.Conflict("Result.InvalidFilePath", "The file path is invalid or does not exist.");
            public static Error FileNotFound(string FileName) => Error.Conflict("Result.FileNotFound", $"The {FileName} was not found.");
        }
    }
}
