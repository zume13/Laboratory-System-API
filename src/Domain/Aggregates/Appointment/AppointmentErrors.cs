using SharedKernel.Shared;
using System.Data;

namespace Domain.Aggregates.Appointment
{
    public static class AppointmentErrors
    {
        public static Error InvalidStatus => Error.Conflict("Appointment.InvalidStatus", "Invalid appointment status.");
        public static Error NotFound => Error.NotFound("Appointment.NotFound", "Appointment not found.");
        public static Error TestNotFound => Error.NotFound("Appointment.TestNotFound", "Appointment test not found.");
        public static Error CannotRemoveLastTest => Error.Conflict("Appointment.CannotRemoveLastTest", "Cannot remove the last test from the appointment.");
        public static Error DuplicateTestCategory => Error.Conflict("Appointment.DuplicateTestCategory", "Cannot add a test with the same category as an existing test in the appointment.");
        public static Error NoTestsProvided => Error.Conflict("Appointment.NoTestsProvided", "At least one test must be provided for the appointment.");
        public static Error TestAlreadyApproved => Error.Conflict("Appointment.TestAlreadyApproved", "The test has already been approved.");
        public static Error TestNotApproved => Error.Conflict("Appointment.TestNotApproved", "The test has not been approved.");
        public static Error AppointmentTestNotFound => Error.NotFound("Appointment.AppointmentTestNotFound", "The appointment test was not found.");
    }
}
