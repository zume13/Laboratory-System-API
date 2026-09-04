
using FluentValidation;

namespace Application.Features.Appointments.Commands.RemoveAppointmentTest
{
    public class RemoveAppointmentTestCommandValidator : AbstractValidator<RemoveAppointmentTestCommand>
    {
        public RemoveAppointmentTestCommandValidator()
        {
            RuleFor(x => x.testCategoryId).NotEmpty().WithMessage("Test Category Id is required.");
            RuleFor(x => x.appointmentId).NotEmpty().WithMessage("Appointment Id is required.");    
        }
    }
}
