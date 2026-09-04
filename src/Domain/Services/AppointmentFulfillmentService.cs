using Domain.Aggregates.Appointment;
using Domain.Aggregates.Laboratory.LaboratoryOrder;
using SharedKernel.Shared;

namespace Domain.Services
{
    public static class AppointmentFulfillmentService
    {
        public static ResultT<LaboratoryRequestOrder> Fulfill(
            Appointment appointment)
        {
            var createOrderResult =
                LaboratoryRequestOrder.Create(
                    appointment.PatientId,
                    appointment.Id);

            if (createOrderResult.IsFailure)
                return createOrderResult.Error;

            var order = createOrderResult.value;

            foreach (var appointmentTest in appointment.Tests)
            {
                if (appointmentTest.isApproved == false)
                    continue;

                var requestResult = order.AddRequest(
                    appointmentTest.TestCategoryId);

                if (requestResult.IsFailure)
                    return requestResult.Error;
            }

            if (order.Requests.Count == 0)
                return LaboratoryOrderErrors.Request.NoRequestsProvided;

            var completeAppointmentResult =
                appointment.Complete();

            if (completeAppointmentResult.IsFailure)
                return completeAppointmentResult.Error;

            return order;
        }
    }
}
