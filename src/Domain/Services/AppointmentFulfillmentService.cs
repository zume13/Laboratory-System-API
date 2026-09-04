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

        }
    }

}
