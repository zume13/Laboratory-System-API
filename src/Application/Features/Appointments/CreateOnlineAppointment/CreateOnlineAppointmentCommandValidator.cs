using FluentValidation;

namespace Application.Features.Appointments.CreateOnlineAppointment
{
    public class CreateOnlineAppointmentCommandValidator : AbstractValidator<CreateOnlineAppointmentCommand>
    {
        public CreateOnlineAppointmentCommandValidator() 
        { 
            RuleFor(x => x.patientId).NotEmpty().WithMessage("PatientId is required.");
            RuleFor(x => x.appointmentSlotId).NotEmpty().WithMessage("AppointmentSlotId is required.");
            RuleFor(x => x.testCategoryId).NotEmpty().WithMessage("TestCategoryId is required.");
        }
    }
}
