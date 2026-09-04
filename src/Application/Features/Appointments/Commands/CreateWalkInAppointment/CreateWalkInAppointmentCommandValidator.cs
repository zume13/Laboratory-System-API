using FluentValidation;

namespace Application.Features.Appointments.Commands.CreateWalkInAppointment
{
    public class CreateWalkInAppointmentCommandValidator : AbstractValidator<CreateWalkInAppointmentCommand>
    {
        public CreateWalkInAppointmentCommandValidator()
        {
            RuleFor(x => x.patientId).NotEmpty().WithMessage("PatientId is required.");
            RuleFor(x => x.appointmentSlotId).NotEmpty().WithMessage("AppointmentSlotId is required.");
            RuleFor(x => x.testCategoryIds).NotEmpty().WithMessage("TestCategoryIds is required.");
        }
    }
}
