using Domain.Aggregates.Appointment;
using Domain.Aggregates.Laboratory.LaboratoryRequest;
using SharedKernel.Shared;


namespace Domain.Services
{
    public static class AppointmentFulfillmentService
    {
        // Converts a confirmed Appointment into an actual diagnostic transaction.
        public static ResultT<LaboratoryRequest> Complete(Appointment appointment, string clinicalDetails)
        {
            var labRequestResult = LaboratoryRequest.CreateForPatient(
                appointment.PatientId,
                appointment.TestCategoryId,
                clinicalDetails,
                appointment.Id);

            if (labRequestResult.IsFailure)
                return labRequestResult.Error;

            var completeResult = appointment.CompleteWithLabRequest(labRequestResult.value.Id);
            if (completeResult.IsFailure)
                return completeResult.Error;

            return labRequestResult.value;
        }
    }

}
