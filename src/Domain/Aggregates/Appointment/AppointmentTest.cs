using LMS.SharedKernel.Primitives;
using SharedKernel.Shared;

namespace Domain.Aggregates.Appointment
{
    public class AppointmentTest : Entity
    {
        private AppointmentTest() { }
        private AppointmentTest(Guid id, Guid appointmentId, Guid testCategoryId)
            : base(id)
        {
            AppointmentId = appointmentId;
            TestCategoryId = testCategoryId;
            isApproved = false;
        }

        public Guid AppointmentId { get; private set; }

        public Guid TestCategoryId { get; private set; }

        public bool isApproved { get; private set; }    

        // internal — only the Appointment aggregate root is allowed to create/mutate
        // these; consumers go through Appointment's methods.
        internal static ResultT<AppointmentTest> Create(Guid appointmentId, Guid testCategoryId)
        {
            if (testCategoryId == Guid.Empty)
                return GeneralErrors.General.Empty(nameof(testCategoryId));

            return new AppointmentTest(Guid.NewGuid(), appointmentId, testCategoryId);
        }

        internal Result Approve()
        {
            if (isApproved)
                return AppointmentErrors.TestAlreadyApproved;

            isApproved = true;

            return Result.Success();
        }

        internal Result Cancel()
        {
            if (!isApproved)
                return AppointmentErrors.TestNotApproved;
            isApproved = false;
            return Result.Success();
        }
    }
}